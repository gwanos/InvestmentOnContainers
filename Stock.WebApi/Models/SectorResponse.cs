using System.Collections.Generic;

namespace Stock.WebApi.Models
{
    public class SectorRelationResponse
    {
        public string Company { get; set; }
        public string Reason { get; set; }
    }

    public class SectorResponse
    {
        public string SectorName { get; set; }
        public List<SectorRelationResponse> RelatedCompanies { get; set; }
    }
}
