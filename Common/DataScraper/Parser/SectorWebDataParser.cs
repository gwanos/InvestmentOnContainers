using System;
using HtmlAgilityPack;
using Common.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Common.Exception;
using Common.Extension;

namespace Common.DataScraper.Parser
{ 
    public class SectorWebDataParser
    {
        public Dictionary<string, int> ParseToSectorTable(HtmlDocument htmlDocument)
        {
            var htmlNodes = htmlDocument.DocumentNode.SelectNodes("//div[@class='table']/table/tbody/tr/td[@class='alL']").ToList();
            var result = htmlNodes.Select(node =>
            {
                return this.getNameAndCode(node);
            }).ToDictionary(x => x.Item1, x => x.Item2);
            return result;
        }

        public Sector ParseToSector(HtmlDocument htmlDocument, int code)
        {
            var htmlNodes = htmlDocument.DocumentNode.SelectNodes("//div[@id='contents']/table/tr").ToList();

            // 섹터명을 파싱하라
            var name = this.getSectorName(htmlNodes[0]);

            // 섹터개요를 파싱하라
            var summary = this.getSectorSummary(htmlNodes[1]);

            // 관련 종목을 파싱하라
            var last = htmlNodes.Count() - 1;
            var companyNodes = htmlNodes.Skip(4);
            var sectorRelations = companyNodes.Select(node => this.getSectorRelation(node, code)).ToList();

            return new Sector(code, name, summary, sectorRelations);
        }

        private Tuple<string, int> getNameAndCode(HtmlNode htmlNode)
        {
            var pattern = "(?<=DetailOpen\\()(.*?)(?=\\);)";
            var match = Regex.Match(htmlNode.InnerHtml, pattern).Value;

            pattern = "(?<=,')(\\d+)(?=')";
            var code = Regex.Match(match, pattern).Value;

            var name = Regex.Replace(match, pattern, string.Empty);
            name = name.RemoveSubstrings("'");
            name = name.ReplaceLast(",", string.Empty);

            return new Tuple<string, int>(name, code.ToInt32());
        }

        private string getSectorName(HtmlNode htmlNode)
        {
            var childNodes = htmlNode.ChildNodes;
            var key = childNodes[1].InnerText.RemoveHtmlTags();
            if (key != "테마명")
            {
                throw new InvalidStructureException();
            }

            return childNodes[3].InnerText.RemoveHtmlTags();
        }

        private string getSectorSummary(HtmlNode htmlNode)
        {
            var childNodes = htmlNode.ChildNodes;
            var key = childNodes[1].InnerText.RemoveHtmlTags();
            if (key != "테마개요")
            {
                throw new InvalidStructureException();
            }

            return childNodes[3].InnerText.RemoveHtmlTags();
        }

        private SectorRelation getSectorRelation(HtmlNode htmlNode, int sectorCode)
        {
            var childNodes = htmlNode.ChildNodes;
            var company = childNodes[1].InnerText.RemoveHtmlTags().ReplaceFirst("-", string.Empty);
            var reason = childNodes[3].InnerText.RemoveHtmlTags();

            var companyCode = Regex.Match(company, "(?<=\\()(\\d+)(?=\\))").Value;
            var companyName = Regex.Match(company, "(.*)(?=\\()").Value;

            var result = new SectorRelation(companyCode.ToInt32(), companyName, sectorCode, reason);
            return result;
        }
    }
}
