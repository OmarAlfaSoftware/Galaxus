using System.Text;
using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Application.Mappings;
using GalaxusIntegration.Application.DTOs.Outgoing;
using GalaxusIntegration.Application.DTOs.PartialDTOs;
using GalaxusIntegration.Infrastructure.Xml.Builders;
using GalaxusIntegration.Infrastructure.Xml.Parsers;
using GalaxusIntegration.Shared.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GalaxusIntegration.Api.Controllers;

[ApiController]
[Route("api/galaxus")]
public class GalaxusXmlController : ControllerBase
{
    private readonly IXmlParser _parser;
    private readonly IGalaxusDocumentMapper _mapper;
    private readonly IXmlBuilder _xmlBuilder;
    private readonly ILogger<GalaxusXmlController> _logger;

    public GalaxusXmlController(
        IXmlParser parser,
        IGalaxusDocumentMapper mapper,
        IXmlBuilder xmlBuilder,
        ILogger<GalaxusXmlController> logger)
    {
        _parser = parser;
        _mapper = mapper;
        _xmlBuilder = xmlBuilder;
        _logger = logger;
    }

    [HttpPost("receive")]
    [Consumes("application/xml", "text/xml")]
    public async Task<IActionResult> ReceiveDocument()
    {
        using var reader = new StreamReader(Request.Body, Encoding.UTF8);
        var xmlContent = await reader.ReadToEndAsync();

        var unifiedDoc = _parser.Parse(xmlContent);
        _logger.LogInformation("Received {Type} with id {Id}", unifiedDoc.DocumentType, unifiedDoc.Header?.Metadata.OrderId ?? unifiedDoc.Header?.Metadata?.DocumentId);

        // For now, just acknowledge receipt. Business processing can be wired as needed.
        return Ok(new
        {
            type = unifiedDoc.DocumentType.ToString(),
            received = true
        });
    }

    [HttpPost("send/{documentType}")]
    public IActionResult SendDocument([FromRoute] DocumentType documentType, [FromBody] UnifiedDocumentDto unified)
    {
        try
        {
            if (unified == null)
                return BadRequest(new { error = "Request body is required" });

            object dto = documentType switch
            {
                DocumentType.ORDER_RESPONSE => BuildOrderResponse(unified),
                // Extend here for other outgoing types when needed
                _ => throw new NotSupportedException($"Sending {documentType} is not yet supported")
            };

            var xml = _xmlBuilder.Build(dto, documentType);
            _logger.LogInformation("Built {Type} xml payload of length {Len}", documentType, xml.Length);

            return Content(xml, "application/xml");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error building {Type} xml", documentType);
            return BadRequest(new { error = ex.Message });
        }
    }

    private OrderResponseDTO BuildOrderResponse(UnifiedDocumentDto unified)
    {
        var items = unified.ItemList?.Items ?? new List<DocumentItem>();
        return new OrderResponseDTO
        {
            OrderResponseHeader = new OrderResponseHeader
            {
                OrderResponseInfo = new OrderResponseInfo
                {
                    OrderId = unified.Header?.Metadata.OrderId,
                    OrderResponseDate = unified.Header?.Metadata?.DocumentDate ?? DateTime.UtcNow,
                    SupplierOrderId = null
                }
            },
            OrderResponseItemList = new OrderResponseItemList
            {
                Items = items.Select(i => new OrderResponseItem
                {
                    ProductId = ToProductDetails(i.ProductDetails),
                    Quantity = i.Quantity ?? 0,
                    // DeliveryDate mapping optional; omit if absent in UnifiedDocumentDto
                    DeliveryDate = null
                }).ToList()
            }
        };
    }

    private GalaxusIntegration.Application.DTOs.PartialDTOs.ProductDetails? ToProductDetails(GalaxusIntegration.Application.DTOs.Internal.ProductDetails? src)
    {
        if (src == null) return null;
        return new GalaxusIntegration.Application.DTOs.PartialDTOs.ProductDetails
        {
            SupplierPid = src.SupplierProductId != null ? new SupplierPid { Type = src.SupplierProductId.Type ?? string.Empty, Value = src.SupplierProductId.Value ?? string.Empty } : null,
            InternationalPid = src.InternationalProductId != null ? new InternationalPid { Type = src.InternationalProductId.Type ?? string.Empty, Value = src.InternationalProductId.Value ?? string.Empty } : null,
            BuyerPid = src.BuyerProductId != null ? new BuyerPid { Type = src.BuyerProductId.Type ?? string.Empty, Value = src.BuyerProductId.Value ?? string.Empty } : null,
            DescriptionShort = src.ShortDescription ?? string.Empty
        };
    }
}
