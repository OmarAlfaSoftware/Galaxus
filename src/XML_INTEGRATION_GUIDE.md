# XML Integration System Guide

## Overview

This document describes the comprehensive XML integration system implemented for the Galaxus integration project. The system handles both incoming and outgoing XML documents with proper namespace management and unified processing.

## Architecture

### Core Components

1. **Unified Document Model** (`UnifiedDocumentDTO`)
   - Single model that handles all document types
   - Reduces code duplication and complexity
   - Supports all 9+ document types (3 incoming, 6+ outgoing)

2. **Dynamic Document Type Registry**
   - Uses dynamic element name generation (e.g., `ORDER_HEADER` from `ORDER`)
   - Centralized configuration for all document types
   - Supports namespace requirements and validation rules

3. **Generic XML Parser**
   - Handles all document types with a single parser
   - Proper namespace management for BMEcat elements
   - Robust error handling and validation

4. **Namespace-Aware XML Builder**
   - Generates XML with correct namespaces
   - Supports all outgoing document types
   - Configurable namespace mappings

## Document Types Supported

### Incoming Documents (3 types)
- `ORDER` - Order from Galaxus
- `RETURNREGISTRATION` - Return registration
- `DISPATCHNOTIFICATION` - Dispatch notification

### Outgoing Documents (6+ types)
- `ORDERRESPONSE` - Order confirmation
- `INVOICE` - Invoice
- `CANCELREQUEST` - Cancel request
- `CANCELCONFIRMATION` - Cancel confirmation
- `SUPPLIERCANCELNOTIFICATION` - Supplier cancel notification
- `SUPPLIERRETURNNOTIFICATION` - Supplier return notification

## Key Features

### 1. Dynamic Element Name Generation
Instead of hardcoding element names, the system generates them dynamically:

```csharp
public class DocumentTypeInfo
{
    public string RootElement { get; set; }
    
    // Dynamic properties
    public string HeaderElement => $"{RootElement}_HEADER";
    public string InfoElement => $"{RootElement}_INFO";
    public string ItemListElement => $"{RootElement}_ITEM_LIST";
    public string ItemElement => $"{RootElement}_ITEM";
    public string SummaryElement => $"{RootElement}_SUMMARY";
}
```

### 2. Unified Processing Pipeline
All documents follow the same processing flow:

1. **Parse** XML to unified model
2. **Identify** document type
3. **Process** using appropriate processor
4. **Generate** response (if needed)
5. **Send** to external system (if outgoing)

### 3. Namespace Management
Proper handling of OpenTrans and BMEcat namespaces:

```csharp
public static class XmlNamespaces
{
    public const string OpenTrans = "http://www.opentrans.org/XMLSchema/2.1";
    public const string BMECat = "http://www.bmecat.org/bmecat/2005";
    public const string XmlSchema = "http://www.w3.org/2001/XMLSchema";
    public const string XmlSchemaInstance = "http://www.w3.org/2001/XMLSchema-instance";
}
```

## Usage Examples

### Receiving an Order

```csharp
[HttpPost("receive")]
public async Task<IActionResult> ReceiveDocument()
{
    // Read XML content
    using var reader = new StreamReader(Request.Body);
    var xmlContent = await reader.ReadToEndAsync();
    
    // Parse to unified model
    var unifiedDoc = _xmlParser.Parse(xmlContent);
    
    // Get appropriate processor
    var processor = _processorFactory.GetProcessor(unifiedDoc.DocumentType);
    
    // Process document
    var result = await processor.ProcessAsync(unifiedDoc);
    
    return result.Success ? Ok(result) : BadRequest(result);
}
```

### Sending an Order Response

```csharp
[HttpPost("send/{documentType}")]
public async Task<IActionResult> SendDocument(
    [FromRoute] DocumentType documentType,
    [FromBody] object requestData)
{
    // Map to outgoing DTO
    var outgoingDto = _mappingOrchestrator.MapToOutgoing(requestData, documentType);
    
    // Build XML with namespaces
    var xmlContent = _xmlBuilder.Build(outgoingDto, documentType);
    
    // Send to external system
    var response = await SendToExternalSystem(xmlContent, documentType);
    
    return Ok(new { success = true, response });
}
```

## Configuration

### appsettings.json
```json
{
  "GalaxusAPI": {
    "BaseUrl": "https://api.galaxus.ch",
    "Endpoints": {
      "ORDER": "https://api.galaxus.ch/order",
      "RETURN": "https://api.galaxus.ch/return"
    },
    "Retry": {
      "MaxAttempts": 3,
      "BackoffMultiplier": 2
    },
    "Validation": {
      "SchemaPath": "Schemas/",
      "EnableStrictValidation": true
    }
  }
}
```

### Dependency Injection
```csharp
// XML integration services
builder.Services.AddSingleton<XmlNamespaceConfig>();
builder.Services.AddSingleton<DocumentTypeRegistry>();
builder.Services.AddScoped<IXmlParser, GenericXmlParser>();
builder.Services.AddScoped<IXmlBuilder, NamespaceAwareXmlBuilder>();

// Document processors
builder.Services.AddScoped<IDocumentProcessor, OrderProcessor>();
builder.Services.AddScoped<IDocumentProcessorFactory, DocumentProcessorFactory>();

// Mapping services
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IMappingOrchestrator, MappingOrchestrator>();

// HTTP client for external API
builder.Services.AddHttpClient("GalaxusAPI", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["GalaxusAPI:BaseUrl"]);
    client.DefaultRequestHeaders.Add("Accept", "application/xml");
});
```

## API Endpoints

### Incoming Documents
- `POST /api/xml/incoming/receive` - Receive and process XML documents
- `POST /api/xml/incoming/validate` - Validate XML documents

### Outgoing Documents
- `POST /api/xml/outgoing/send/{documentType}` - Send XML documents to external system

## Testing

The system includes comprehensive tests:

```csharp
[Fact]
public void Should_Parse_Order_Document_To_Unified_Model()
{
    // Arrange
    var xmlContent = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<ORDER version=""2.1"" type=""standard"">
    <ORDER_HEADER>
        <ORDER_INFO>
            <ORDER_ID>12345</ORDER_ID>
            <ORDER_DATE>2024-01-01</ORDER_DATE>
        </ORDER_INFO>
    </ORDER_HEADER>
</ORDER>";

    // Act
    var result = _parser.Parse(xmlContent);

    // Assert
    Assert.Equal(DocumentType.ORDER, result.DocumentType);
    Assert.Equal("12345", result.Header.Info.OrderId);
}
```

## Benefits

1. **Maintainability** - Single unified model reduces code duplication
2. **Extensibility** - Easy to add new document types
3. **Reliability** - Robust error handling and validation
4. **Performance** - Efficient parsing and processing
5. **Standards Compliance** - Proper namespace handling for OpenTrans/BMEcat

## Future Enhancements

1. **Schema Validation** - Add XSD validation for each document type
2. **Caching** - Cache parsed documents for performance
3. **Retry Logic** - Implement exponential backoff for failed requests
4. **Audit Trail** - Log all document processing activities
5. **Monitoring** - Add metrics and health checks

## Troubleshooting

### Common Issues

1. **Namespace Errors** - Ensure proper namespace configuration in `XmlNamespaceConfig`
2. **Missing Elements** - Check required fields in `DocumentTypeConstants`
3. **Mapping Errors** - Verify AutoMapper profiles are correctly configured
4. **HTTP Errors** - Check external API configuration and connectivity

### Debugging

Enable detailed logging in `appsettings.json`:
```json
{
  "Logging": {
    "LogLevel": {
      "GalaxusIntegration.Infrastructure.Xml": "Debug",
      "GalaxusIntegration.Application.Services": "Debug"
    }
  }
}
```

## Conclusion

This XML integration system provides a robust, maintainable, and extensible solution for handling Galaxus XML documents. It follows clean architecture principles and provides a solid foundation for future enhancements.
