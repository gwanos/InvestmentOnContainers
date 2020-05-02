using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using News.WebApi.Models;

namespace News.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KeywordController : ControllerBase
    {
        private readonly ILogger<KeywordController> _logger;

        public KeywordController(ILogger<KeywordController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public NewsResponse Get(
            [FromQuery(Name = "keyword")] string keyword)
        {
            // todo:
            return new NewsResponse() { Titles = new List<string> { $"News sample about {keyword}." } };
        }
    }
}
