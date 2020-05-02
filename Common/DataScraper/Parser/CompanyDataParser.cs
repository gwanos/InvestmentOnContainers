using System.Collections.Generic;
using Common.Domain;
using HtmlAgilityPack;

namespace Common.DataScraper.Parser
{
    public class CompanyDataParser
    {
        public Dictionary<string, Company> ParseToCompanyTable(HtmlDocument htmlDocument)
        {
            var result = new Dictionary<string, Company>();
            var htmlNodes = htmlDocument.DocumentNode.SelectNodes("//body/table/tr");

            foreach (var row in htmlNodes)
            {
                var datas = row.SelectNodes("td");
                if (datas.IsNull())
                {
                    continue;
                }
                var company = new Company(
                    industry: datas[2].InnerText,
                    name: datas[0].InnerText,
                    majorProduct: datas[3].InnerText);
                company.ToPublic(new Stock(datas[1].InnerText));
                result.AddOrUpdate(company.Name, company);
            }

            return result;
        }
    }
}
