using System;
using Common.DataScraper.Crawler;
using Common.DataScraper.Parser;
using Common.DataScraper.Scraper;
using Common.DataTable;
using Common.Service;
using Microsoft.Extensions.DependencyInjection;

namespace FunctionalTests.Fixture
{
    public class CompanyFixture
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public CompanyFixture()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<IFileDataCrawler, FileDataCrawler>();
            serviceCollection.AddTransient<IWebDataCrawler, WebDataCrawler>();
            serviceCollection.AddTransient<CompanyDataParser>();
            serviceCollection.AddTransient<FinancialDataParser>();
            serviceCollection.AddTransient<ICompanyDataScraper, CompanyDataScraper>();
            serviceCollection.AddTransient<IFinancialDataScraper, FinancialDataScraper>();

            serviceCollection.AddSingleton<ICompanyTable, CompanyTable>();
            serviceCollection.AddTransient<ICompanyService, CompanyService>();

            this.ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
