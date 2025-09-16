using Microsoft.AspNetCore.Mvc;

namespace GalaxusIntegration.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet("health")]
        public IActionResult Health() => Ok("Healthy");

        [HttpGet("health/ready")]
        public IActionResult Ready() => Ok("Ready");

        [HttpGet("health/live")]
        public IActionResult Live() => Ok("Live");
    }
}