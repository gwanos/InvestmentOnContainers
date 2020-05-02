using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Common.DataScraper.Crawler
{
    public interface IWebDataCrawler
    {
        HtmlDocument Crawl(string url);
        Task<HtmlDocument> CrawlAsync(string url);
    }

    public class WebDataCrawler : IWebDataCrawler
    {
        private const int EncodingKor = 51949;

        public async Task<HtmlDocument> CrawlAsync(string url)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var htmlWeb = new HtmlWeb();
            var document = await htmlWeb.LoadFromWebAsync(url, Encoding.GetEncoding(EncodingKor)).ConfigureAwait(false);

            return document;
        }

        public HtmlDocument Crawl(string url)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var htmlWeb = new HtmlWeb();
            htmlWeb.OverrideEncoding = Encoding.GetEncoding(EncodingKor);
            var document = htmlWeb.Load(url);

            return document;
        }
    }
}
