using System;
using Xunit;
using FunctionalTests.Fixture;
using Common.Service;
using Microsoft.Extensions.DependencyInjection;

namespace FunctionalTests.Test
{
    public class CompanyTest : IClassFixture<CompanyFixture>
    {
        private IServiceProvider _serviceProvider;
        private ICompanyService _companyService;

        public CompanyTest(CompanyFixture companyFixture)
        {
            _serviceProvider = companyFixture.ServiceProvider;
            _companyService = _serviceProvider.GetRequiredService<ICompanyService>();
        }

        [InlineData("엔씨소프트")]
        [Theory]
        public void GetCompany(string name)
        {
            //
            var company = _companyService.GetCompany(name);

            //
            Assert.Equal(name, company.Name);
        }
    }
}
