using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace hygiea.web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PreauthorizationController : ControllerBase
    {
        private readonly ILogger<PreauthorizationController> _logger;

        public PreauthorizationController(ILogger<PreauthorizationController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult PostAuthorization()
        {
            return BadRequest();
        }
    }
}
