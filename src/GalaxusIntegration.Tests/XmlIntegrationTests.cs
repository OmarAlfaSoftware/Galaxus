using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Infrastructure.Xml.Configuration;
using GalaxusIntegration.Infrastructure.Xml.Parsers;
using GalaxusIntegration.Shared.Enum;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GalaxusIntegration.Tests;

public class XmlIntegrationTests
{
    private readonly Mock<ILogger<GenericXmlParser>> _loggerMock;
    private readonly DocumentTypeRegistry _registry;
    private readonly GenericXmlParser _parser;

    public XmlIntegrationTests()
    {
        _loggerMock = new Mock<ILogger<GenericXmlParser>>();
        _registry = new DocumentTypeRegistry();
        _parser = new GenericXmlParser(_loggerMock.Object, _registry);
    }

    [Fact]
    public void Should_Identify_Order_Document_Type()
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
        var documentType = _parser.IdentifyDocumentType(xmlContent);

        // Assert
        Assert.Equal(DocumentType.ORDER, documentType);
    }

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
            <LANGUAGE>en</LANGUAGE>
            <CURRENCY>CHF</CURRENCY>
        </ORDER_INFO>
    </ORDER_HEADER>
    <ORDER_ITEM_LIST>
        <ORDER_ITEM>
            <LINE_ITEM_ID>1</LINE_ITEM_ID>
            <QUANTITY>2</QUANTITY>
            <ORDER_UNIT>C62</ORDER_UNIT>
        </ORDER_ITEM>
    </ORDER_ITEM_LIST>
    <ORDER_SUMMARY>
        <TOTAL_ITEM_NUM>1</TOTAL_ITEM_NUM>
        <TOTAL_AMOUNT>100.00</TOTAL_AMOUNT>
    </ORDER_SUMMARY>
</ORDER>";

        // Act
        var result = _parser.Parse(xmlContent);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(DocumentType.ORDER, result.DocumentType);
        Assert.Equal("2.1", result.Version);
        Assert.Equal("standard", result.SubType);
        Assert.Equal("12345", result.Header.Metadata.OrderId);
        Assert.Equal(1, result.ItemList.Items.Count);
        Assert.Equal(1, result.Summary.TotalItemCount);
        Assert.Equal(100.00m, result.Summary.TotalGrossAmount);
    }

    [Fact]
    public void Should_Identify_Return_Registration_Document_Type()
    {
        // Arrange
        var xmlContent = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<RETURNREGISTRATION version=""2.1"" type=""standard"">
    <RETURNREGISTRATION_HEADER>
        <RETURNREGISTRATION_INFO>
            <ORDER_ID>12345</ORDER_ID>
            <RETURNREGISTRATION_ID>RET001</RETURNREGISTRATION_ID>
        </RETURNREGISTRATION_INFO>
    </RETURNREGISTRATION_HEADER>
</RETURNREGISTRATION>";

        // Act
        var documentType = _parser.IdentifyDocumentType(xmlContent);

        // Assert
        Assert.Equal(DocumentType.RETURN_REGISTRATION, documentType);
    }

    [Fact]
    public void Should_Handle_Missing_Optional_Elements()
    {
        // Arrange
        var xmlContent = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<ORDER version=""2.1"" type=""standard"">
    <ORDER_HEADER>
        <ORDER_INFO>
            <ORDER_ID>12345</ORDER_ID>
        </ORDER_INFO>
    </ORDER_HEADER>
</ORDER>";

        // Act
        var result = _parser.Parse(xmlContent);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(DocumentType.ORDER, result.DocumentType);
        Assert.Equal("12345", result.Header.Metadata.OrderId);
        // Should not throw for missing optional elements
    }
}
