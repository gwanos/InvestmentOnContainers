using System.Collections.Concurrent;
using System.Collections.Generic;
using Common.DataScraper.Scraper;
using Common.Domain;

namespace Common.DataTable
{
    public interface ICompanyTable
    {
        Company Get(string name);
        void AddOrUpdate(Company company);
        ConcurrentDictionary<string, Company> GetTable();
    }

    public class CompanyTable : ICompanyTable
    {
        private ConcurrentDictionary<string, Company> _table;
        private ICompanyDataScraper _companyDataScraper;

        public CompanyTable(ICompanyDataScraper companyDataScraper)
        {
            _companyDataScraper = companyDataScraper;
            this.initializeTables();
        }

        public Company Get(string name)
        {
            return _table.GetValueOrDefault(name);
        }

        public void AddOrUpdate(Company company)
        {
            _table.AddOrUpdate(company.Name, company);
        }

        public ConcurrentDictionary<string, Company> GetTable()
        {
            return _table;
        }

        private void initializeTables()
        {
            _table = new ConcurrentDictionary<string, Company>(_companyDataScraper.ScrapCompanyTable());
        }
    }
}
