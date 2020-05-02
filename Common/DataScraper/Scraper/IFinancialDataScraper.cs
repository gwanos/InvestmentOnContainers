using System.Threading.Tasks;
using Common.DataScraper.Crawler;
using Common.DataScraper.Parser;
using Common.Domain;

namespace Common.DataScraper.Scraper
{
    public interface IFinancialDataScraper
    {
        FinancialStatement ScrapeFinancialStatement(string stockCode);
        Task<FinancialStatement> ScrapeFinancialStatementAsync(string stockCode);
    }

    public class FinancialDataScraper : IFinancialDataScraper
    {
        private const string _mainUrl = "https://finance.naver.com/item/main.nhn?code={0}";
        private const string _subUrl = "https://navercomp.wisereport.co.kr/v2/company/c1010001.aspx?cmp_cd={0}&cn=";

        private IWebDataCrawler _crawler;
        private FinancialDataParser _parser;

        public FinancialDataScraper(IWebDataCrawler crawler, FinancialDataParser parser)
        {
            _crawler = crawler;
            _parser = parser;
        }

        public FinancialStatement ScrapeFinancialStatement(string stockCode)
        {
            //try
            //{
                //
                var mainHtmlDoc = _crawler.Crawl(string.Format(_mainUrl, stockCode));
                var subHtmlDoc = _crawler.Crawl(string.Format(_subUrl, stockCode));
                
                //
                return _parser.ToFinancialStatement(mainHtmlDoc, subHtmlDoc);
            //}
            //catch(System.Exception e)
            //{
            //    //System.Diagnostics.Debug.WriteLine(stockCode);
            //    //System.Diagnostics.Debug.WriteLine(e);
            //    //throw e;
            //}
        }

        public async Task<FinancialStatement> ScrapeFinancialStatementAsync(string stockCode)
        {
            // Crawling
            var mainHtmlDoc = await _crawler.CrawlAsync(string.Format(_mainUrl, stockCode)).ConfigureAwait(false);
            var subHtmlDoc = await _crawler.CrawlAsync(string.Format(_subUrl, stockCode)).ConfigureAwait(false);

            // Parsing
            return _parser.ToFinancialStatement(mainHtmlDoc, subHtmlDoc);
        }
    }
}