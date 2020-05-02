using System;
using Common.DataScraper.Crawler;
using Common.DataScraper.Parser;
using Common.DataScraper.Scraper;
using Common.DataTable;
using Common.Service;
using Microsoft.Extensions.DependencyInjection;

namespace FunctionalTests.Fixture
{
    public class SectorFixture
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public SectorFixture()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<IWebDataCrawler, WebDataCrawler>();
            serviceCollection.AddTransient<SectorWebDataParser>();
            serviceCollection.AddTransient<ISectorDataScraper, SectorWebDataScraper>();

            serviceCollection.AddSingleton<ISectorTable, SectorTable>();
            serviceCollection.AddTransient<ISectorService, SectorService>();
            this.ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
