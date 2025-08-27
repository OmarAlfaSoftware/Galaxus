using Microsoft.AspNetCore.Mvc;

namespace GalaxusIntegration.Api.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetOrders()
        {
            // Return list of orders
            return Ok();
        }

        [HttpPost("{id}/dispatch")]
        public IActionResult DispatchOrder(string id)
        {
            // Dispatch order
            return Accepted();
        }

        [HttpPost("{id}/invoice")]
        public IActionResult GenerateInvoice(string id)
        {
            // Generate invoice for order
            return Accepted();
        }
    }
}