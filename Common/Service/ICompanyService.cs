using System;
using System.Collections.Generic;
using Common.Domain;
using Common.DataScraper.Scraper;
using Common.DataTable;

namespace Common.Service
{
    public interface ICompanyService
    {
        Company GetCompany(string name);
        Dictionary<string, Company> GetCompanyTable();
    }

    public class CompanyService : ICompanyService
    {
        private ICompanyTable _table;
        private IFinancialDataScraper _financialDataScraper;

        public CompanyService(ICompanyTable table, IFinancialDataScraper financialDataScraper)
        {
            _table = table;
            _financialDataScraper = financialDataScraper;
        }

        public Company GetCompany(string name)
        {
            var company = _table.Get(name);
            if (company.FinancialStatement.IsNotNull())
            {
                return company;
            }
            
            var financialStatement = _financialDataScraper.ScrapeFinancialStatement(company.Stock.Code);
            company.UpdateFinancialStatement(financialStatement);
            _table.AddOrUpdate(company);

            var aa = _table.Get(name);
            return company;
        }

        public Dictionary<string, Company> GetCompanyTable()
        {
            return new Dictionary<string, Company>(_table.GetTable());
        }
    }
}
