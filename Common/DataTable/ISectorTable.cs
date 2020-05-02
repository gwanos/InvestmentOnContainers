using System.Collections.Generic;
using System.Linq;
using Common.DataScraper.Scraper;
using Common.Domain;
using Common.Extension;

namespace Common.DataTable
{
    public interface ISectorTable
    {
        public Sector GetSector(int sectorCode);
        public string GetSectorName(int sectorCode);
        public int GetSectorCode(string sectorName);
        public Dictionary<int, Sector> GetSectorTable();
    }

    public class SectorTable : ISectorTable
    {
        private Dictionary<int, string> _sectorCodeTable;
        private Dictionary<string, int> _sectorNameTable;
        private Dictionary<int, Sector> _sectorDetailTable;
        private ISectorDataScraper _dataScraper;

        public SectorTable(ISectorDataScraper dataScraper)
        {
            _dataScraper = dataScraper;
            this.initializeTables();
        }

        public Dictionary<int, Sector> GetSectorTable()
        {
            return _sectorDetailTable;
        }

        public Sector GetSector(int sectorCode)
        {
            bool doesKeyExist = _sectorDetailTable.ContainsKey(sectorCode);
            if (doesKeyExist == false)
            {
                return null;
            }

            return _sectorDetailTable[sectorCode];
        }

        public int GetSectorCode(string sectorName)
        {
            bool doesKeyExist = _sectorNameTable.ContainsKey(sectorName);
            if (doesKeyExist == false)
            {
                return -1;
            }

            return _sectorNameTable[sectorName];
        }

        public string GetSectorName(int sectorCode)
        {
            bool doesKeyExist = _sectorCodeTable.ContainsKey(sectorCode);
            if (doesKeyExist == false)
            {
                return null;
            }

            return _sectorCodeTable[sectorCode];
        }

        private void initializeTables()
        {
            _sectorNameTable = _dataScraper.ScrapeSectorTable();
            _sectorCodeTable = _sectorNameTable.ReverseKeyValue();
            _sectorDetailTable = _sectorCodeTable.Select(kv =>
            {
                return _dataScraper.ScrapeSectorData(kv.Key);
            }).ToDictionary(sector => sector.Code);
        }
    }
}
