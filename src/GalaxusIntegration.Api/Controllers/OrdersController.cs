using GalaxusIntegration.Application.DTOs.Order_Coming_Requests;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Xml.Serialization;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;

    public OrderController(ILogger<OrderController> logger)
    {
        _logger = logger;
    }

    [HttpPost("receive")]
    [Consumes("application/xml", "text/xml")]
    [Produces("application/json")]
    public async Task<IActionResult> ReceiveOrder()
    {
        try
        {
            // Read the raw XML for debugging
            Request.EnableBuffering();
            using var reader = new StreamReader(Request.Body, Encoding.UTF8, leaveOpen: true);
            var xmlContent = await reader.ReadToEndAsync();
            Request.Body.Position = 0; // Reset for model binding

            _logger.LogInformation($"Received XML (first 500 chars): {xmlContent.Substring(0, Math.Min(500, xmlContent.Length))}");

            // Try to deserialize manually for better error messages
            var serializer = new XmlSerializer(typeof(ReceiveOrderDTO));
            ReceiveOrderDTO order;

            using (var stringReader = new StringReader(xmlContent))
            {
                order = (ReceiveOrderDTO)serializer.Deserialize(stringReader);
            }

            if (order == null)
            {
                return BadRequest("Failed to deserialize XML");
            }

            _logger.LogInformation($"Successfully received order: {order.OrderHeader?.OrderInfo?.OrderId}");

            return Ok(new
            {
                success = true,
                orderId = order.OrderHeader?.OrderInfo?.OrderId,
                itemCount = order.OrderItemList?.Items?.Count ?? 0,
                receivedAt = DateTime.UtcNow
            });
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "XML Deserialization error");
            return BadRequest(new { error = "Invalid XML structure", details = ex.InnerException?.Message ?? ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing order");
            return StatusCode(500, new { error = "Failed to process order", message = ex.Message });
        }
    }
}