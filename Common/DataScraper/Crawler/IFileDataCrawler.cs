using System.Text;
using HtmlAgilityPack;

namespace Common.DataScraper.Crawler
{
    public interface IFileDataCrawler
    {
        HtmlDocument Crawl(string path);
    }

    public class FileDataCrawler : IFileDataCrawler
    {
        public HtmlDocument Crawl(string filePath)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var doc = new HtmlDocument();
            doc.Load(filePath, Encoding.GetEncoding(51949));    // EUC-KR
            return doc;
        }
    }
}
