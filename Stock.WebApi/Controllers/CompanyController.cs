using Common.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stock.WebApi.Models;

namespace Stock.WebApi.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly ICompanyService _service;

        public CompanyController(ILogger<CompanyController> logger, ICompanyService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public FinancialStatementResponse Get(
            [FromQuery(Name = "name")] string name)
        {
            var company = _service.GetCompany(name);

            return new FinancialStatementResponse()
            {
                BusinessProfits = company.FinancialStatement.BusinessProfits,
                ReserveRatio = company.FinancialStatement.ReserveRatio,
                Pbr = company.FinancialStatement.Pbr,
                Per = company.FinancialStatement.Per,
                EvEbitda = company.FinancialStatement.EvEbitda
            };
        }
    }
}
