using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Common.DataScraper.Crawler;
using Common.DataScraper.Parser;
using Common.Domain;

namespace Common.DataScraper.Scraper
{
    public interface ICompanyDataScraper
    {
        Dictionary<string, Company> ScrapCompanyTable();
    }

    public class CompanyDataScraper : ICompanyDataScraper
    {
        private IFileDataCrawler _crawler;
        private CompanyDataParser _parser;

        private readonly string _file;

        public CompanyDataScraper(IFileDataCrawler crawler, CompanyDataParser parser)
        {
            _crawler = crawler;
            _parser = parser;

            //Assembly asm = Assembly.GetExecutingAssembly();
            var root = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            //var root = Directory.GetCurrentDirectory();
            _file = Path.Combine(root, "DataTable", "Files", "PublicCompanies.html");
        }

        public Dictionary<string, Company> ScrapCompanyTable()
        {
            // @크롤러
            // 회사 정보를 가져오라
            // 파라미터: 파일 경로, 반환값: HtmlDocument
            var htmlDoc = _crawler.Crawl(_file);

            // @파서
            // 크롤러가 가져온 데이터를 정제하라
            // 파라미터: HtmlDocument, 반환값: 회사명, 사업분야, 주력상품
            return _parser.ParseToCompanyTable(htmlDoc);
        }
    }
}
