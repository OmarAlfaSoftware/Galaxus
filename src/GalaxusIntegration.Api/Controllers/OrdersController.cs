using GalaxusIntegration.Application.DTOs.Order_Coming_Requests;
using GalaxusIntegration.Application.Factories;
using GalaxusIntegration.Application.Models;
using GalaxusIntegration.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Xml.Serialization;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly IDocumentProcessorFactory _processorFactory;
    private readonly IXmlParserService _xmlParser;

    public OrderController(ILogger<OrderController> logger,IDocumentProcessorFactory processor, IXmlParserService _xmlParserService)
    {
        _logger = logger;
        _processorFactory = processor;
        _xmlParser = _xmlParserService;
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
    [HttpPost("receiveDocument")]
    [Consumes("application/xml", "text/xml")]
    [Produces("application/json")]
    public async Task<IActionResult> ReceiveDocument()
    {

        try
        {
            using var reader = new StreamReader(Request.Body);
            var xmlContent = await reader.ReadToEndAsync();

            // Parse XML to unified model
            var document = _xmlParser.ParseDocument(xmlContent);

            // Process based on type
            var processor = _processorFactory.GetProcessor(document.DocumentType);
            var result = await processor.ProcessAsync(document);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing document");
            return BadRequest(new { error = ex.Message });
        }
    }
}