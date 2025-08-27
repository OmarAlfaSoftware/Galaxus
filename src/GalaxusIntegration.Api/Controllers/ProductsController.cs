using Microsoft.AspNetCore.Mvc;

namespace GalaxusIntegration.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            // Return paginated list of products
            return Ok();
        }

        [HttpPost("sync")]
        public IActionResult SyncProducts()
        {
            // Trigger product catalog sync
            return Accepted();
        }

        [HttpGet("sync-status")]
        public IActionResult GetSyncStatus()
        {
            // Return sync status
            return Ok();
        }
    }
}