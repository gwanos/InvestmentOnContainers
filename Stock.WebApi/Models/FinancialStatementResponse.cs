namespace Stock.WebApi.Models
{
    public class FinancialStatementResponse
    {
        public string BusinessProfits { get; set; } 
        public string EvEbitda { get; set; }
        public string Pbr { get; set; }
        public string Per { get; set; }
        public string ReserveRatio { get; set; }
    }
}
