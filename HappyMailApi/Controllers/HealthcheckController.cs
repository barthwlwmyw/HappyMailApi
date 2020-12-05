using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace HappyMailApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthcheckController : ControllerBase
    {
        private readonly ILogger<HealthcheckController> _logger;

        public HealthcheckController(ILogger<HealthcheckController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok($"HappyMail 💛📧 API is up and running ({DateTime.UtcNow.ToString()})");
        }
    }
}
