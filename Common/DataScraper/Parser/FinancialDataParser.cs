using Common.Domain;
using Common.Exception;
using Common.Extension;
using HtmlAgilityPack;

namespace Common.DataScraper.Parser
{
    public class FinancialDataParser
    {
        public FinancialStatement ToFinancialStatement(HtmlDocument mainHtmlDoc, HtmlDocument subHtmlDoc)
        {
            var result = new FinancialStatement();
            var htmlNodes = mainHtmlDoc.DocumentNode
                            .SelectNodes("//table[@class='tb_type1 tb_num tb_type1_ifrs']/tbody/tr");
            if (htmlNodes.IsNullOrEmpty())
            {
                return result;
            }
            // 영업이익, 유보을 알아오라
            result.BusinessProfits = this.getBusinessProfit(htmlNodes[1]);
            result.ReserveRatio = this.getReserveRatio(htmlNodes[8]);

            // PER, PBR, EV/EVITDA를 알아오라
            htmlNodes = subHtmlDoc.DocumentNode
                            .SelectNodes("//div[@class='fund fl_le']/table/tbody/tr");
            if (htmlNodes.IsNullOrEmpty())
            {
                return result;
            }

            result.Per = this.getPer(htmlNodes[0]);
            result.Pbr = this.getPbr(htmlNodes[1]);
            result.EvEbitda = this.getEvebitda(htmlNodes[3]);

            return result;
        }

        public string getBusinessProfit(HtmlNode htmlNode)
        {
            var key = htmlNode.SelectSingleNode("th").InnerText;
            if (key != "영업이익")
            {
                throw new InvalidStructureException();
            }

            var datas = htmlNode.SelectNodes("td");
            if (datas.IsNullOrEmpty())
            {
                return string.Empty;
            }

            var result = datas[2].InnerText.RemoveSubstrings("\n", "\t", "\r");
            return result;
        }

        public string getReserveRatio(HtmlNode htmlNode)
        {
            var key = htmlNode.SelectSingleNode("th").InnerText;
            if (key != "유보율")
            {
                throw new InvalidStructureException();
            }

            var datas = htmlNode.SelectNodes("td");
            if (datas.IsNullOrEmpty())
            {
                return string.Empty;
            }
            var result = datas[2].InnerText.RemoveSubstrings("\n", "\t", "\r");
            return result;
        }

        public string getPbr(HtmlNode htmlNode)
        {
            var key = htmlNode.SelectSingleNode("th").InnerText;
            if (key != "PBR")
            {
                throw new InvalidStructureException();
            }

            var datas = htmlNode.SelectNodes("td");
            if (datas.IsNullOrEmpty())
            {
                return string.Empty;
            }
            var result = datas[0].InnerText;
            return result;
        }

        public string getPer(HtmlNode htmlNode)
        {
            var key = htmlNode.SelectSingleNode("th").InnerText;
            if (key != "PER")
            {
                throw new InvalidStructureException();
            }

            var datas = htmlNode.SelectNodes("td");
            if (datas.IsNullOrEmpty())
            {
                return string.Empty;
            }
            var result = datas[0].InnerText;
            return result;
        }

        public string getEvebitda(HtmlNode htmlNode)
        {
            var key = htmlNode.SelectSingleNode("th").InnerText;
            if (key != "EV/EBITDA")
            {
                throw new InvalidStructureException();
            }

            var datas = htmlNode.SelectNodes("td");
            if (datas.IsNullOrEmpty())
            {
                return string.Empty;
            }
            var result = datas[0].InnerText;
            return result;
        }
    }
}
