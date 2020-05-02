using System;
using FunctionalTests.Fixture;
using Xunit;
using Common.Service;
using Microsoft.Extensions.DependencyInjection;

namespace FunctionalTests.Test
{
    public class SectorTest : IClassFixture<SectorFixture>
    {
        private IServiceProvider _serviceProvider;
        private ISectorService _sectorService;
        
        public SectorTest(SectorFixture sectorFixture)
        {
            _serviceProvider = sectorFixture.ServiceProvider;
            _sectorService = _serviceProvider.GetRequiredService<ISectorService>();
        }
        
        [InlineData("게임")]
        [Theory]
        public void GetSector(string name)
        {
            //
            var code = _sectorService.GetSectorCode(name);
            var sector = _sectorService.GetSector(code);

            //
            Assert.Equal(name, sector.Name);
            Assert.NotNull(sector.RelatedCompany);
        }

        [Fact]
        public void GetSectorTable()
        {
            //
            var table = _sectorService.GetSectorTable();

            //
            Assert.NotEmpty(table);
        }
    }
}
