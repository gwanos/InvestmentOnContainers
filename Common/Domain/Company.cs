namespace Common.Domain
{
    public class Company
    {
        public FinancialStatement FinancialStatement { get; private set; }
        public string Industry { get; private set; }
        public string Name { get; private set; }
        public string MajorProduct { get; private set; }
        public Stock Stock { get; private set; }

        public Company(string industry, string name, string majorProduct)
        {
            this.Industry = industry;
            this.Name = name;
            this.MajorProduct = majorProduct;
        }

        /// <summary>
        /// 재무제표를 갱신하라
        /// </summary>
        /// <param name="financialStatement"></param>
        public void UpdateFinancialStatement(FinancialStatement financialStatement)
        {
            this.FinancialStatement = financialStatement;
        }

        /// <summary>
        /// 상장하라
        /// </summary>
        /// <param name="stock"></param>
        public void ToPublic(Stock stock)
        {
            this.Stock = stock;
        }
    }
}
