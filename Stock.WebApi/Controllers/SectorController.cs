using System.Linq;
using Common.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stock.WebApi.Models;

namespace Stock.WebApi.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class SectorController : ControllerBase
    {
        private readonly ILogger<SectorController> _logger;
        private readonly ISectorService _service;

        public SectorController(ILogger<SectorController> logger, ISectorService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public SectorResponse Get(
            [FromQuery(Name = "name")] string name)
        {
            var code = _service.GetSectorCode(name);
            if (code == -1)
            {
                // todo: 섹터가 존재하지 않을 경우 응답 처리
            }

            var sector = _service.GetSector(code);
            var relatedCompanies = sector.RelatedCompany.Select(company => new SectorRelationResponse()
            {
                Company = company.CompanyName,
                Reason = company.Reason
            }).ToList();
            return new SectorResponse()
            {
                SectorName = sector.Name,
                RelatedCompanies = relatedCompanies
            };
        }
    }
}