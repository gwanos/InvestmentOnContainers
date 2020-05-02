namespace Common.Domain
{
    public class SectorRelation
    {
        public int CompanyCode { get; private set; }
        public string CompanyName { get; private set; }
        public int SectorCode { get; private set; }
        public string Reason { get; private set; }

        public SectorRelation(int companyCode, string companyName, int sectorCode, string reason)
        {
            this.CompanyCode = companyCode;
            this.CompanyName = companyName;
            this.SectorCode = sectorCode;
            this.Reason = reason;
        }
    }
}
