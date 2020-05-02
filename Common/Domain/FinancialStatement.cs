using System;

namespace Common.Domain
{
    public class FinancialStatement
    {
        public string BusinessProfits { get; set; }    // 영업이익 (단위: 억원) 
        public string EvEbitda { get; set; }
        public string Pbr { get; set; }
        public string Per { get; set; }
        public string ReserveRatio { get; set; }     // 유보율. ((이익잉여금 + 지본잉여금) / 납입자본금) * 100
    }
}