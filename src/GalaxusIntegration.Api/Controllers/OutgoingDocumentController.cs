using System.Text;
using GalaxusIntegration.Application.Mappings;
using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Infrastructure.Xml.Builders;
using GalaxusIntegration.Shared.Enum;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace GalaxusIntegration.Api.Controllers;

[ApiController]
[Route("api/xml/outgoing")]
public class OutgoingDocumentController : ControllerBase
{
    private readonly IGalaxusDocumentMapper _documentMapper;
    private readonly IXmlBuilder _xmlBuilder;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<OutgoingDocumentController> _logger;
    
    public OutgoingDocumentController(
        IGalaxusDocumentMapper documentMapper,
        IXmlBuilder xmlBuilder,
        IHttpClientFactory httpClientFactory,
        ILogger<OutgoingDocumentController> logger)
    {
        _documentMapper = documentMapper;
        _xmlBuilder = xmlBuilder;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }
    
    [HttpPost("send/{documentType}")]
    public async Task<IActionResult> SendDocument(
        [FromRoute] DocumentType documentType,
        [FromBody] UnifiedGalaxusDocument unified)
    {
        try
        {
            // Map to outgoing DTO
            var outgoingDto = _documentMapper.MapFromUnified(unified, documentType);
            
            // Build XML with namespaces
            var xmlContent = _xmlBuilder.Build(outgoingDto, documentType);
            
            _logger.LogInformation($"Sending {documentType} document");
            
            // Send to external system
            var response = await SendToExternalSystem(xmlContent, documentType);
            
            return Ok(new
            {
                success = true,
                documentType,
                response
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error sending {documentType} document");
            return StatusCode(500, new { error = ex.Message });
        }
    }
    
    private async Task<string> SendToExternalSystem(string xmlContent, DocumentType documentType)
    {
        var client = _httpClientFactory.CreateClient("GalaxusAPI");
        
        var content = new StringContent(xmlContent, Encoding.UTF8, "application/xml");
        
        var endpoint = GetEndpointForDocumentType(documentType);
        var response = await client.PostAsync(endpoint, content);
        
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadAsStringAsync();
    }
    
    private string GetEndpointForDocumentType(DocumentType type)
    {
        return type switch
        {
            DocumentType.ORDERRESPONSE => "/api/order/response",
            DocumentType.INVOICE => "/api/invoice/create",
            DocumentType.CANCELREQUEST => "/api/cancel/request",
            DocumentType.CANCELCONFIRMATION => "/api/cancel/confirmation",
            DocumentType.SUPPLIERCANCELNOTIFICATION => "/api/supplier/cancel",
            DocumentType.SUPPLIERRETURNNOTIFICATION => "/api/supplier/return",
            _ => throw new NotSupportedException($"No endpoint configured for {type}")
        };
    }
}
