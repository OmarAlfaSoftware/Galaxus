using System.IO;
using System.Threading.Tasks;
using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Application.Factories;
using GalaxusIntegration.Application.Services.Processors;
using GalaxusIntegration.Infrastructure.Xml.Parsers;
using GalaxusIntegration.Shared.Enum;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using GalaxusIntegration.Application.Mappings;

namespace GalaxusIntegration.Api.Controllers;

[ApiController]
[Route("api/xml/incoming")]
public class IncomingDocumentController : ControllerBase
{
    private readonly IXmlParser _xmlParser;
    private readonly IDocumentProcessorFactory _processorFactory;
    private readonly ILogger<IncomingDocumentController> _logger;
    
    public IncomingDocumentController(
        IXmlParser xmlParser,
        IDocumentProcessorFactory processorFactory,
        ILogger<IncomingDocumentController> logger)
    {
        _xmlParser = xmlParser;
        _processorFactory = processorFactory;
        _logger = logger;
    }
    
    [HttpPost("receive")]
    [Consumes("application/xml", "text/xml")]
    [Produces("application/json")]
    public async Task<IActionResult> ReceiveDocument()
    {
        try
        {
            // Read XML content
            using var reader = new StreamReader(Request.Body);
            var xmlContent = await reader.ReadToEndAsync();
            
            _logger.LogInformation("Received XML document");
            
            // Parse to unified model
            var unifiedDoc = _xmlParser.Parse(xmlContent);
            
            // Get appropriate processor
            var processor = _processorFactory.GetProcessor(unifiedDoc.DocumentType);
            
            // Process document
            var result = await processor.ProcessAsync(unifiedDoc);
            
            if (result.Success)
            {
                return Ok(result);
            }
            
            return BadRequest(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing incoming document");
            return StatusCode(500, new { error = ex.Message });
        }
    }
    
    [HttpPost("validate")]
    [Consumes("application/xml")]
    public async Task<IActionResult> ValidateDocument()
    {
        try
        {
            using var reader = new StreamReader(Request.Body);
            var xmlContent = await reader.ReadToEndAsync();
            
            // Identify document type
            var documentType = _xmlParser.IdentifyDocumentType(xmlContent);
            
            // Validate against schema
            // var validationResult = await _schemaValidator.ValidateAsync(xmlContent, documentType);
            
            return Ok(new { documentType, isValid = true });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message, isValid = false });
        }
    }
}
/*AutoMapper.AutoMapperMappingException
     HResult=0x80131500
     Message=Error mapping types.
     Source=AutoMapper
     StackTrace:
      at GalaxusIntegration.Application.Services.Processors.OrderProcessor.<ProcessAsync>d__4.MoveNext() in E:\Omar\Galaxus\src\GalaxusIntegration.Application\Services\Processors\OrderProcessor.cs:line 32
   
     This exception was originally thrown at this call stack:
       [External Code]
   
   Inner Exception 1:
   ArgumentException: Requested value 'standard' was not found.
   */