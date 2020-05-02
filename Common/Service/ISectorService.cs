using System.Collections.Generic;
using Common.Domain;
using Common.DataTable;

namespace Common.Service
{
    public interface ISectorService
    {
        public Sector GetSector(int sectorCode);
        public int GetSectorCode(string sectorName);
        public string GetSectorName(int sectorCode);
        public Dictionary<int, Sector> GetSectorTable();
    }

    public class SectorService : ISectorService
    {
        private ISectorTable _sectorTable;

        public SectorService(ISectorTable sectorTable)
        {
            _sectorTable = sectorTable;
        }

        public Sector GetSector(int sectorCode)
        {
            return _sectorTable.GetSector(sectorCode);
        }

        public int GetSectorCode(string sectorName)
        {
            return _sectorTable.GetSectorCode(sectorName);
        }

        public string GetSectorName(int sectorCode)
        {
            return _sectorTable.GetSectorName(sectorCode);
        }

        public Dictionary<int, Sector> GetSectorTable()
        {
            return _sectorTable.GetSectorTable();
        }
    }
}
