using System.Collections.Generic;

namespace Common.Domain
{
    public class Stock
    {
        public string Code { get; private set; }
        public List<int> RelatedSectors { get; private set; } = new List<int>();

        public Stock(string code)
        {
            this.Code = code;
        }

        public void AddSector(int sectorCode)
        {
            RelatedSectors.Add(sectorCode);
        }

        public void RemoveSector(int sectorCode)
        {
            RelatedSectors.Remove(sectorCode);
        }
    }
}
