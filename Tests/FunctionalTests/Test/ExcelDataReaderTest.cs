using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.WebApi.Models;
using HtmlAgilityPack;
using Xunit;
using Common.Domain;

namespace FunctionalTests.Test
{
    public class ExcelDataReaderTest
    {
        private int _defaultEncoding = 51949;   // EUC-KR

        [Fact]
        public void ReadKRXHtml()
        {
            //
            var path = @"/Users/gwanos/Downloads/ipos2.html";
            var doc = new HtmlDocument();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            doc.Load(path: path, encoding: Encoding.GetEncoding(_defaultEncoding));
            var tableRows = doc.DocumentNode.SelectNodes("//body/table/tr");

            //
            var result = new Dictionary<string, Company>();
            foreach (var row in tableRows)
            {
                var datas = row.SelectNodes("td");
                if (datas.IsNull())
                {
                    continue;
                }
                var corporationData = new Company(
                    name: datas[0].InnerText,
                    industry: datas[2].InnerText,
                    majorProduct: datas[3].InnerText);
                result.AddOrUpdate(corporationData.Name, corporationData);
            }

            //
            Assert.NotEmpty(result);
        }

        [Fact]
        public void ReadNaverFinanceHtml()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            //var projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent;
            //var path = $"{projectPath}/Datas/NaverFinance.html";
            //var doc = new HtmlDocument();
            //doc.Load(path: path);

            var url = $"https://navercomp.wisereport.co.kr/v2/company/c1010001.aspx?cmp_cd=000660&cn=";
            var htmlWeb = new HtmlWeb();
            var doc = htmlWeb.Load(url);
            //var documentNode = htmlDoc.DocumentNode;


            var fundamentalTables = doc.DocumentNode
                            .SelectNodes("//table[@class='gHead03']")
                            .Where(x => x.Id != "earning_list")
                            .ToList();
            //tables.RemoveWhere(x => x.Id == "earning_list");
            var fundamentalTableBody = fundamentalTables.Select(x => x.SelectSingleNode("tbody")).FirstOrDefault();
            var fundamentalTableRows = fundamentalTableBody.SelectNodes("tr");

            var summary = new FinancialStatement()
            {
                Pbr = fundamentalTableRows.getData("PBR"),//.ToFloat(),
                Per = fundamentalTableRows.getData("PER"),//.ToFloat(),
                EvEbitda = fundamentalTableRows.getData("EV/EBITDA")//.ToFloat()
            };

            url = "https://finance.naver.com/item/main.nhn?code=000660";
            htmlWeb = new HtmlWeb();
            doc = htmlWeb.Load(url);
            var financialTables = doc.DocumentNode
                            .SelectNodes("//table[@class='tb_type1 tb_num tb_type1_ifrs']")
                            .ToList();
            var financialTableBody = financialTables.Select(x => x.SelectSingleNode("tbody")).FirstOrDefault();
            var financialTableRows = financialTableBody.SelectNodes("tr");

            summary.BusinessProfits = financialTableRows.Where(x => x.InnerHtml.Contains("영업이익"))
                                                        .FirstOrDefault()
                                                        .SelectNodes("td")[2].InnerText.removeCharacter();//.ToInt32();
            summary.ReserveRatio = financialTableRows.Where(x => x.InnerHtml.Contains("유보율"))
                                                     .FirstOrDefault()
                                                     .SelectNodes("td")[2].InnerText.removeCharacter();//.ToFloat();
            return;
        }


    }

    public static class Extension
    {
        public static string getData(this HtmlNodeCollection nodeCollection, string text)
        {
            return nodeCollection.Where(x => x.InnerHtml.Contains(text))
                               .FirstOrDefault()
                               .SelectSingleNode("td[@class='num']")
                               .InnerText;
        }

        public static string removeCharacter(this string text)
        {
            text = text.Replace("\n", string.Empty);
            text = text.Replace("\t", string.Empty);
            text = text.Replace("\r", string.Empty);
            text = text.Replace(",", string.Empty);

            return text;
        }
    }
}
