using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Infrastructure.Xml.Configuration;
using Microsoft.Extensions.Logging;
using GalaxusIntegration.Shared.Constants;
using GalaxusIntegration.Shared.Enum;

namespace GalaxusIntegration.Infrastructure.Xml.Parsers;

public class GenericXmlParser : IXmlParser
{
    private readonly ILogger<GenericXmlParser> _logger;
    private readonly DocumentTypeRegistry _typeRegistry;

    public GenericXmlParser(
        ILogger<GenericXmlParser> logger,
        DocumentTypeRegistry typeRegistry)
    {
        _logger = logger;
        _typeRegistry = typeRegistry;
    }

    public UnifiedDocumentDTO Parse(string xmlContent)
    {
        var documentType = IdentifyDocumentType(xmlContent);
        var doc = XDocument.Parse(xmlContent);
        var root = doc.Root;

        var unifiedDoc = new UnifiedDocumentDTO
        {
            DocumentType = documentType,
            Version = root.Attribute("version")?.Value,
            Type = root.Attribute("type")?.Value
        };
        
        ParseDocumentByType(root, unifiedDoc, documentType);
        
        return unifiedDoc;
    }
    
    public T Parse<T>(string xmlContent) where T : class
    {
        var serializer = new XmlSerializer(typeof(T));
        using var stringReader = new StringReader(xmlContent);
        return serializer.Deserialize(stringReader) as T;
    }
    
    public DocumentType IdentifyDocumentType(string xmlContent)
    {
        var doc = XDocument.Parse(xmlContent);
        var rootName = doc.Root.Name.LocalName;
        
        return _typeRegistry.GetDocumentTypeByRootElement(rootName);
    }
    
    private void ParseDocumentByType(XElement root, UnifiedDocumentDTO doc, DocumentType type)
    {
        var typeInfo = GalaxusIntegration.Shared.Constants.DocumentTypeConstants.DocumentTypeInfoMap[type];
        
        // Parse header
        var headerElement = root.Element(XName.Get(typeInfo.HeaderElement, XmlNamespaces.OpenTrans));
        if (headerElement != null)
        {
            doc.Header = ParseHeader(headerElement, typeInfo);
        }
        
        // Parse item list
        var itemListElement = root.Element(XName.Get(typeInfo.ItemListElement, XmlNamespaces.OpenTrans));
        if (itemListElement != null)
        {
            doc.ItemList = ParseItemList(itemListElement, typeInfo);
        }
        
        // Parse summary
        var summaryElement = root.Element(XName.Get(typeInfo.SummaryElement, XmlNamespaces.OpenTrans));
        if (summaryElement != null)
        {
            doc.Summary = ParseSummary(summaryElement);
        }
    }
    
    private DocumentHeader ParseHeader(XElement headerElement, GalaxusIntegration.Shared.Constants.DocumentTypeInfo typeInfo)
    {
        var header = new DocumentHeader();
        
        // Parse CONTROL_INFO
        var controlInfo = headerElement.Element(XName.Get("CONTROL_INFO", XmlNamespaces.OpenTrans));
        if (controlInfo != null)
        {
            header.ControlInfo = new ControlInfo
            {
                GenerationDate = ParseDateTime(controlInfo.Element(XName.Get("GENERATION_DATE", XmlNamespaces.OpenTrans))?.Value)
            };
        }
        
        // Parse INFO element
        var infoElement = headerElement.Element(XName.Get(typeInfo.InfoElement, XmlNamespaces.OpenTrans));
        if (infoElement != null)
        {
            header.Info = ParseDocumentInfo(infoElement, typeInfo);
        }
        
        return header;
    }
    
    private DocumentInfo ParseDocumentInfo(XElement infoElement, GalaxusIntegration.Shared.Constants.DocumentTypeInfo typeInfo)
    {
        var info = new DocumentInfo();
        
        // Parse common fields
        info.OrderId = GetElementValue(infoElement, "ORDER_ID");
        info.Language = GetElementValue(infoElement, "LANGUAGE", XmlNamespaces.BMECat);
        info.Currency = GetElementValue(infoElement, "CURRENCY", XmlNamespaces.BMECat);
        
        
        // Parse date based on document type
        switch (typeInfo.Direction)
        {
            case DocumentDirection.Incoming:
                info.OrderDate = ParseDateTime(GetElementValue(infoElement, "ORDER_DATE"));
                break;
            case DocumentDirection.Outgoing:
                info.DocumentDate = ParseDateTime(GetElementValue(infoElement, "RESPONSE_DATE"));
                break;
        }
        
        // Parse parties
        var partiesElement = infoElement.Element(XName.Get("PARTIES", XmlNamespaces.OpenTrans));
        if (partiesElement != null)
        {
            info.Parties = ParseParties(partiesElement);
        }
        var headerudx=infoElement.Element(XName.Get("HEADER_UDX", XmlNamespaces.OpenTrans));
        if (headerudx != null)
        {
            info.HeaderUDX = ParseHeaderUDX(headerudx);
        }
        var customerOrderRef=infoElement.Element(XName.Get("CUSTOMER_ORDER_REFERENCE", XmlNamespaces.OpenTrans));
        if (customerOrderRef != null)
        {
            info.CustomerOrderReference = ParseCustomerOrderReference(customerOrderRef);
        }
        var orderParties= infoElement.Element(XName.Get("ORDER_PARTIES_REFERENCE", XmlNamespaces.OpenTrans));
        if (orderParties != null)
        {
            info.OrderPartiesReference = ParseOrderPartiesReference(orderParties);
        }

        return info;
    }
    private OrderPartiesReference ParseOrderPartiesReference(XElement orderPartiesRefElement)
    {
        return new OrderPartiesReference
        {
            BuyerIdRef = GetElementValue(orderPartiesRefElement, "BUYER_IDREF", XmlNamespaces.BMECat),
            SupplierIdRef = GetElementValue(orderPartiesRefElement, "SUPPLIER_IDREF", XmlNamespaces.BMECat),

        };
    }
    private CustomerOrderRefernce ParseCustomerOrderReference(XElement customerOrderRefElement)
    {
        return new CustomerOrderRefernce
        {
            OrderId = GetElementValue(customerOrderRefElement, "ORDER_ID"),
        };
    }
    private HeaderUDX ParseHeaderUDX(XElement headerUdxElement)
    {
        return new HeaderUDX
        {
            CustomerType = GetElementValue(headerUdxElement, "UDX.DG.CUSTOMER_TYPE"),
            DeliveryType = GetElementValue(headerUdxElement, "UDX.DG.DELIVERY_TYPE"),
            SaturdayDeliveryAllowed = GetElementValue(headerUdxElement, "UDX.DG.IS_SATURDAY_ALLOWED") == "true",
            IsCollectiveOrder = GetElementValue(headerUdxElement, "UDX.DG.IS_COLLECTIVE_ORDER") == "true",
            EndCustomerOrderReference = GetElementValue(headerUdxElement, "UDX.DG.CUSTOMER_ORDER_REF"),
            PhysicalDeliveryNoteRequired = GetElementValue(headerUdxElement, "UDX.DG.PHYSICAL_DELIVERY_NOTE_REQUIRED") == "Required"
        };
    }

    private Parties ParseParties(XElement partiesElement)
    {
        var parties = new Parties
        {
            PartyList = new List<Party>()
        };
        
        foreach (var partyElement in partiesElement.Elements(XName.Get("PARTY", XmlNamespaces.OpenTrans)))
        {
            parties.PartyList.Add(ParseParty(partyElement));
        }
        
        return parties;
    }
    
    private Party ParseParty(XElement partyElement)
    {
        var party = new Party
        {
            PartyRole = GetElementValue(partyElement, "PARTY_ROLE"),
                PartyIds = new List<PartyId>()
            };

        // Parse multiple PARTY_ID elements
        foreach (var partyIdElement in partyElement.Elements(XName.Get("PARTY_ID", XmlNamespaces.BMECat)))
            {
                party.PartyIds.Add(new PartyId
                {
                Type = partyIdElement.Attribute("type")?.Value,
                Value = partyIdElement.Value
            });
        }
        
        // Parse address
        var addressElement = partyElement.Element(XName.Get("ADDRESS", XmlNamespaces.OpenTrans));
        if (addressElement != null)
        {
            party.Address = ParseAddress(addressElement);
        }
        
        return party;
    }
    
    private Address ParseAddress(XElement addressElement)
    {
        return new Address
        {
            Name = GetElementValue(addressElement, "NAME", XmlNamespaces.BMECat),
            Street = GetElementValue(addressElement, "STREET", XmlNamespaces.BMECat),
            City = GetElementValue(addressElement, "CITY", XmlNamespaces.BMECat),
            Zip = GetElementValue(addressElement, "ZIP", XmlNamespaces.BMECat),
            Country = GetElementValue(addressElement, "COUNTRY", XmlNamespaces.BMECat),
            Phone = GetElementValue(addressElement, "PHONE", XmlNamespaces.BMECat),
            Email = GetElementValue(addressElement, "EMAIL", XmlNamespaces.BMECat)
        };
    }
    
    private DocumentItemList ParseItemList(XElement itemListElement, GalaxusIntegration.Shared.Constants.DocumentTypeInfo typeInfo)
    {
        var itemList = new DocumentItemList
        {
            Items = new List<DocumentItem>()
        };
        
        foreach (var itemElement in itemListElement.Elements(XName.Get(typeInfo.ItemElement, XmlNamespaces.OpenTrans)))
        {
            itemList.Items.Add(ParseItem(itemElement));
        }
        
        return itemList;
    }
    
    private DocumentItem ParseItem(XElement itemElement)
    {
        var item = new DocumentItem
        {
            LineItemId = GetElementValue(itemElement, "LINE_ITEM_ID"),
            Quantity = ParseDecimal(GetElementValue(itemElement, "QUANTITY")),
            OrderUnit = GetElementValue(itemElement, "ORDER_UNIT", XmlNamespaces.BMECat),
            PriceLineAmount = ParseDecimal(GetElementValue(itemElement, "PRICE_LINE_AMOUNT")),
            ReturnReason = ParseInt(GetElementValue(itemElement, "RETURNREASON"))
        };

        // Parse PRODUCT_ID
        var productIdElement = itemElement.Element(XName.Get("PRODUCT_ID", XmlNamespaces.OpenTrans));
        if (productIdElement != null)
        {
            item.ProductId = ParseProductDetails(productIdElement);
        }

        // Parse PRODUCT_PRICE_FIX
        var priceElement = itemElement.Element(XName.Get("PRODUCT_PRICE_FIX", XmlNamespaces.OpenTrans));
        if (priceElement != null)
        {
            item.ProductPriceFix = ParseProductPriceFix(priceElement);
        }

        return item;
    }

    private ProductDetails ParseProductDetails(XElement productIdElement)
    {
        return new ProductDetails
        {
            SupplierPid = ParsePidWithType(productIdElement, "SUPPLIER_PID"),
            InternationalPid = ParsePidWithType(productIdElement, "INTERNATIONAL_PID"),
            BuyerPid = ParsePidWithType(productIdElement, "BUYER_PID"),
            DescriptionShort = GetElementValue(productIdElement, "DESCRIPTION_SHORT", XmlNamespaces.BMECat)
        };
    }

    private PidWithType? ParsePidWithType(XElement parent, string elementName)
    {
        var element = parent.Element(XName.Get(elementName, XmlNamespaces.BMECat));
        if (element == null) return null;

        return new PidWithType
        {
            Type = element.Attribute("type")?.Value,
            Value = element.Value
        };
    }

    private ProductPriceFix ParseProductPriceFix(XElement priceElement)
    {
        return new ProductPriceFix
        {
            Amount = ParseDecimal(GetElementValue(priceElement, "PRICE_AMOUNT", XmlNamespaces.BMECat)) ?? 0,
            Currency = GetElementValue(priceElement, "CURRENCY", XmlNamespaces.BMECat)
        };
    }
    
    private DocumentSummary ParseSummary(XElement summaryElement)
    {
        return new DocumentSummary
        {
            TotalItemNum = ParseInt(GetElementValue(summaryElement, "TOTAL_ITEM_NUM")) ?? 0,
            TotalAmount = ParseDecimal(GetElementValue(summaryElement, "TOTAL_AMOUNT"))
        };
    }
    
    // Helper methods
    private string GetElementValue(XElement parent, string elementName, string nameSpace = XmlNamespaces.OpenTrans)
    {
        return parent.Element(XName.Get(elementName, nameSpace))?.Value;
    }
    
    private DateTime? ParseDateTime(string value)
    {
        return DateTime.TryParse(value, out var result) ? result : null;
    }
    
    private decimal? ParseDecimal(string value)
    {
        return decimal.TryParse(value, out var result) ? result : null;
    }
    
    private int? ParseInt(string value)
    {
        return int.TryParse(value, out var result) ? result : null;
    }
}
