using Common.DataScraper.Crawler;
using Common.DataScraper.Parser;
using Common.DataScraper.Scraper;
using Common.DataTable;
using Common.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Stock.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // common
            services.AddTransient<IFileDataCrawler, FileDataCrawler>();
            services.AddTransient<IWebDataCrawler, WebDataCrawler>();
        
            // company
            services.AddTransient<CompanyDataParser>();
            services.AddTransient<FinancialDataParser>();
            services.AddTransient<ICompanyDataScraper, CompanyDataScraper>();
            services.AddTransient<IFinancialDataScraper, FinancialDataScraper>();
            services.AddSingleton<ICompanyTable, CompanyTable>();
            services.AddTransient<ICompanyService, CompanyService>();

            // sector
            services.AddTransient<SectorWebDataParser>();
            services.AddTransient<ISectorDataScraper, SectorWebDataScraper>();
            services.AddSingleton<ISectorTable, SectorTable>();
            services.AddTransient<ISectorService, SectorService>();

            // controller
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UsePathBase("/stock");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
