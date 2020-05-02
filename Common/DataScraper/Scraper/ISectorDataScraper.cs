using System.Collections.Generic;
using Common.DataScraper.Crawler;
using Common.DataScraper.Parser;
using Common.Domain;

namespace Common.DataScraper.Scraper
{
    public interface ISectorDataScraper
    {
        public Sector ScrapeSectorData(int sectorCode);
        public Dictionary<string, int> ScrapeSectorTable();
    }

    public class SectorWebDataScraper : ISectorDataScraper
    {
        // todo: read from setting file
        private const string _sectorListUrl = "http://m.infostock.co.kr/sector/sector.asp?mode=r";
        private const string _sectorDetailUrl = "http://m.infostock.co.kr/sector/sector_detail.asp?code={0}";

        private readonly IWebDataCrawler _crawler;
        private readonly SectorWebDataParser _parser;

        public SectorWebDataScraper(IWebDataCrawler crawler, SectorWebDataParser parser)
        {
            _crawler = crawler;
            _parser = parser;
        }

        public Sector ScrapeSectorData(int sectorCode)
        {
            // @크롤러
            // 구체적인 섹터 정보를 가져오라
            // 파라미터: 섹터코드, 반환값: HtmlDocument
            var url = _sectorDetailUrl.Format(sectorCode);
            var htmlDoc = _crawler.Crawl(url);

            // @파서
            // 크롤러가 가져온 데이터를 정제하라
            // 파라미터: HtmlDocument, 반환값: 섹터명, 섹터코드, 섹터 개요, 연관 기업, 섹터 편입 이유
            return _parser.ParseToSector(htmlDoc, sectorCode);
        }

        public Dictionary<string, int> ScrapeSectorTable()
        {
            // @크롤러
            // 모든 섹터 리스트를 가져오라
            // 파라미터: 없음, 반환값: HtmlDocument
            var htmlDoc = _crawler.Crawl(_sectorListUrl);

            // @파서
            // 크롤러가 가져온 데이터를 정제하라
            // 파라미터: HtmlDocument, 반환값: 섹터명, 섹터코드
            return _parser.ParseToSectorTable(htmlDoc);
        }
    }
}
