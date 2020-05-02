using System.Collections.Generic;

namespace Common.Domain
{
    public class Sector
    {
        public int Code { get; private set; }
        public string Name { get; private set; }
        public string Summary { get; private set; }
        public List<SectorRelation> RelatedCompany { get; private set; }

        public Sector(int code, string name, string summary, List<SectorRelation> relatedCompany)
        {
            this.Code = code;
            this.Name = name;
            this.Summary = summary;
            this.RelatedCompany = relatedCompany;
        }
    }
}
