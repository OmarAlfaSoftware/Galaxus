using System.Text;
using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Application.Mappings;
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
        _logger.LogInformation("Received {Type} with id {Id}", unifiedDoc.DocumentType, unifiedDoc.Header?.Info?.OrderId ?? unifiedDoc.Header?.Info?.DocumentId);

        // For now, just acknowledge receipt. Business processing can be wired as needed.
        return Ok(new
        {
            type = unifiedDoc.DocumentType.ToString(),
            received = true
        });
    }

    [HttpPost("send/{documentType}")]
    public IActionResult SendDocument([FromRoute] DocumentType documentType, [FromBody] UnifiedDocumentDTO unified)
    {
        // For now, just return a stub response since the legacy mapper is not compatible
        // In a real implementation, you would use the new MappingOrchestrator
        _logger.LogInformation("Sending {Type} document", documentType);
        return Ok(new { 
            documentType, 
            message = "Document processing not yet implemented with new XML integration system",
            received = true 
        });
    }
}
