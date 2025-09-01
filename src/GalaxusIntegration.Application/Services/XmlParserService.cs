
using GalaxusIntegration.Core.Entities;
using global::GalaxusIntegration.Application.DTOs.PartialDTOs;
using global::GalaxusIntegration.Application.Models;
using Microsoft.Extensions.Logging;
using System.Xml;
using System.Xml.Serialization;

namespace GalaxusIntegration.Application.Services;

public interface IXmlParserService
{
    OpenTransDocument ParseDocument(string xmlContent);
}

public class XmlParserService : IXmlParserService
{
    private readonly ILogger<XmlParserService> _logger;


    public XmlParserService(ILogger<XmlParserService> logger)
    {
        _logger = logger;
    }

    public OpenTransDocument ParseDocument(string xmlContent)
    {
        var doc = new XmlDocument();
        doc.LoadXml(xmlContent);

        var root = doc.DocumentElement;
        var documentType = root.LocalName; // ORDER, RETURNREGISTRATION, etc.

        _logger.LogInformation($"Parsing {documentType} document");

        var result = new OpenTransDocument
        {
            DocumentType = documentType,
            Version = root.GetAttribute("version"),
            Type = root.GetAttribute("type")
        };

        // Parse based on document type
        switch (documentType)
        {
            case "ORDER":
                ParseOrderDocument(root, result);
                break;
            case "RETURNREGISTRATION":
                ParseReturnDocument(root, result);
                break;
            case "DISPATCHNOTIFICATION":
                ParseDispatchDocument(root, result);
                break;
            // Add other types...
            default:
                throw new NotSupportedException($"Document type {documentType} not supported");
        }

        return result;
    }

    private void ParseOrderDocument(XmlNode root, OpenTransDocument doc)
    {
        doc.Header = new DocumentHeader();

        // Get ORDER_HEADER
        var headerNode = root.SelectSingleNode("*[local-name()='ORDER_HEADER']");
        if (headerNode != null)
        {
            ParseHeader(headerNode, doc.Header, "ORDER_INFO");
        }

        // Get ORDER_ITEM_LIST
        var itemListNode = root.SelectSingleNode("*[local-name()='ORDER_ITEM_LIST']");
        if (itemListNode != null)
        {
            doc.ItemList = ParseItemList(itemListNode, "ORDER_ITEM");
        }

        // Get ORDER_SUMMARY
        var summaryNode = root.SelectSingleNode("*[local-name()='ORDER_SUMMARY']");
        if (summaryNode != null)
        {
            doc.Summary = ParseSummary(summaryNode);
        }

    }

    private void ParseReturnDocument(XmlNode root, OpenTransDocument doc)
    {
        doc.Header = new DocumentHeader();

        // Get RETURNREGISTRATION_HEADER
        var headerNode = root.SelectSingleNode("*[local-name()='RETURNREGISTRATION_HEADER']");
        if (headerNode != null)
        {
            ParseHeader(headerNode, doc.Header, "RETURNREGISTRATION_INFO");
        }

        // Get RETURNREGISTRATION_ITEM_LIST
        var itemListNode = root.SelectSingleNode("*[local-name()='RETURNREGISTRATION_ITEM_LIST']");
        if (itemListNode != null)
        {
            doc.ItemList = ParseItemList(itemListNode, "RETURNREGISTRATION_ITEM");
        }

        // Get RETURNREGISTRATION_SUMMARY
        var summaryNode = root.SelectSingleNode("*[local-name()='RETURNREGISTRATION_SUMMARY']");
        if (summaryNode != null)
        {
            doc.Summary = ParseSummary(summaryNode);
        }
    }

    private void ParseDispatchDocument(XmlNode root, OpenTransDocument doc)
    {
        // Similar pattern for dispatch notification
        doc.Header = new DocumentHeader();

        var headerNode = root.SelectSingleNode("*[local-name()='DISPATCHNOTIFICATION_HEADER']");
        if (headerNode != null)
        {
            ParseHeader(headerNode, doc.Header, "DISPATCHNOTIFICATION_INFO");
        }

        // Continue with item list and summary...
    }

    private void ParseHeader(XmlNode headerNode, DocumentHeader header, string infoNodeName)
    {
        // Parse CONTROL_INFO if exists
        var controlNode = headerNode.SelectSingleNode("*[local-name()='CONTROL_INFO']");
        if (controlNode != null)
        {
            header.ControlInfo = new ControlInfo
            {
                GenerationDate = ParseDateTime(GetNodeValue(controlNode, "GENERATION_DATE"))?? DateTime.MinValue
            };
        }

        // Parse INFO node (ORDER_INFO, RETURNREGISTRATION_INFO, etc.)
        var infoNode = headerNode.SelectSingleNode($"*[local-name()='{infoNodeName}']");
        if (infoNode != null)
        {
            header.Info = ParseDocumentInfo(infoNode);
        }
    }


    private Parties ParseParties(XmlNode partiesNode)
    {
        var parties = new Parties { PartyList = new List<Party>() };

        foreach (XmlNode partyNode in partiesNode.SelectNodes("*[local-name()='PARTY']"))
        {
            var party = new Party
            {
                PartyRole = GetNodeValue(partyNode, "PARTY_ROLE"),
                PartyIds = new List<PartyId>()
            };

            // Parse multiple PARTY_ID elements
            foreach (XmlNode partyIdNode in partyNode.SelectNodes("*[local-name()='PARTY_ID']"))
            {
                party.PartyIds.Add(new PartyId
                {
                    Type = partyIdNode.Attributes["type"]?.Value,
                    Value = partyIdNode.InnerText
                });
            }

            // Parse ADDRESS
            var addressNode = partyNode.SelectSingleNode("*[local-name()='ADDRESS']");
            if (addressNode != null)
            {
                party.Address = ParseAddress(addressNode);
            }

            parties.PartyList.Add(party);
        }

        return parties;
    }

    private Address ParseAddress(XmlNode addressNode)
    {
        var address = new Address
        {
            Name = GetNodeValue(addressNode, "NAME"),
            Name2 = GetNodeValue(addressNode, "NAME2"),
            Name3 = GetNodeValue(addressNode, "NAME3"),
            Department = GetNodeValue(addressNode, "DEPARTMENT"),
            Street = GetNodeValue(addressNode, "STREET"),
            Zip = GetNodeValue(addressNode, "ZIP"),
            BoxNo = GetNodeValue(addressNode, "BOXNO"),
            City = GetNodeValue(addressNode, "CITY"),
            Country = GetNodeValue(addressNode, "COUNTRY"),
            CountryCoded = GetNodeValue(addressNode, "COUNTRY_CODED"),
            Phone = GetNodeValue(addressNode, "PHONE"),
            Email = GetNodeValue(addressNode, "EMAIL"),
            VatId = GetNodeValue(addressNode, "VAT_ID")
        };

        // Parse CONTACT_DETAILS if exists
        var contactNode = addressNode.SelectSingleNode("*[local-name()='CONTACT_DETAILS']");
        if (contactNode != null)
        {
            address.ContactDetails = new ContactDetails
            {
                Title = GetNodeValue(contactNode, "TITLE"),
                FirstName = GetNodeValue(contactNode, "FIRST_NAME"),
                ContactName = GetNodeValue(contactNode, "CONTACT_NAME")
            };
        }

        return address;
    }


    private DocumentSummary ParseSummary(XmlNode summaryNode)
    {
        return new DocumentSummary
        {
            TotalItemNum = ParseInt(GetNodeValue(summaryNode, "TOTAL_ITEM_NUM")) ?? 0,
            TotalAmount = ParseDecimal(GetNodeValue(summaryNode, "TOTAL_AMOUNT"))
        };
    }

    // Helper methods
    private string GetNodeValue(XmlNode parent, string nodeName)
    {
        return parent.SelectSingleNode($"*[local-name()='{nodeName}']")?.InnerText;
    }

    private DateTime? ParseDateTime(string value)
    {
        return DateTime.TryParse(value, out var result) ? result : null;
    }

    private double? ParseDecimal(string value)
    {
        return double.TryParse(value, out var result) ? result : null;
    }

    private int? ParseInt(string value)
    {
        return int.TryParse(value, out var result) ? result : null;
    }

    private DocumentItemList ParseItemList(XmlNode itemListNode, string itemNodeName)
    {
        var itemList = new DocumentItemList { Items = new List<DocumentItem>() };

        foreach (XmlNode itemNode in itemListNode.SelectNodes($"*[local-name()='{itemNodeName}']"))
        {
            var item = new DocumentItem
            {
                LineItemId = GetNodeValue(itemNode, "LINE_ITEM_ID"),
                Quantity = ParseDecimal(GetNodeValue(itemNode, "QUANTITY"))??0,
                OrderUnit = GetNodeValue(itemNode, "ORDER_UNIT"),
                PriceLineAmount = ParseDecimal(GetNodeValue(itemNode, "PRICE_LINE_AMOUNT")),
                ReturnReason = ParseInt(GetNodeValue(itemNode, "RETURNREASON"))
            };

            // Parse PRODUCT_ID
            var productIdNode = itemNode.SelectSingleNode("*[local-name()='PRODUCT_ID']");
            if (productIdNode != null)
            {
                item.ProductId = ParseProductId(productIdNode);
            }

            // Parse PRODUCT_PRICE_FIX
            var priceNode = itemNode.SelectSingleNode("*[local-name()='PRODUCT_PRICE_FIX']");
            if (priceNode != null)
            {
                item.ProductPriceFix = ParseProductPriceFix(priceNode);
            }

            // Parse DELIVERY_DATE
            var deliveryDateNode = itemNode.SelectSingleNode("*[local-name()='DELIVERY_DATE']");
            if (deliveryDateNode != null)
            {
                item.DeliveryDate = ParseDeliveryDate(deliveryDateNode);
            }

            itemList.Items.Add(item);
        }

        return itemList;
    }
    private DocumentInfo ParseDocumentInfo(XmlNode infoNode)
    {
        var info = new DocumentInfo
        {
            OrderId = GetNodeValue(infoNode, "ORDER_ID"),
            Language = GetNodeValue(infoNode, "LANGUAGE"),
            Currency = GetNodeValue(infoNode, "CURRENCY"),
            ReturnRegistrationId = GetNodeValue(infoNode, "RETURNREGISTRATION_ID"),
            DispatchNotificationId = GetNodeValue(infoNode, "DISPATCHNOTIFICATION_ID"),
            ShipmentId = GetNodeValue(infoNode, "SHIPMENT_ID"),
            TrackingTracingUrl = GetNodeValue(infoNode, "TRACKING_TRACING_URL")
        };

        // Parse date based on which one exists
        var dateValue = GetNodeValue(infoNode, "ORDER_DATE")
                        ?? GetNodeValue(infoNode, "RETURNREGISTRATION_DATE")
                        ?? GetNodeValue(infoNode, "DISPATCHNOTIFICATION_DATE");
        if (!string.IsNullOrEmpty(dateValue))
        {
            info.Date = ParseDateTime(dateValue);
        }

        // Parse PARTIES
        var partiesNode = infoNode.SelectSingleNode("*[local-name()='PARTIES']");
        if (partiesNode != null)
        {
            info.Parties = ParseParties(partiesNode);
        }

        // Parse CUSTOMER_ORDER_REFERENCE
        var customerOrderRefNode = infoNode.SelectSingleNode("*[local-name()='CUSTOMER_ORDER_REFERENCE']");
        if (customerOrderRefNode != null)
        {
            info.CustomerOrderReference = ParseCustomerOrderReference(customerOrderRefNode);
        }

        // Parse ORDER_PARTIES_REFERENCE
        var orderPartiesRefNode = infoNode.SelectSingleNode("*[local-name()='ORDER_PARTIES_REFERENCE']");
        if (orderPartiesRefNode != null)
        {
            info.OrderPartiesReference = ParseOrderPartiesReference(orderPartiesRefNode);
        }

        // Parse HEADER_UDX
        var udxNode = infoNode.SelectSingleNode("*[local-name()='HEADER_UDX']");
        if (udxNode != null)
        {
            info.HeaderUDX = ParseHeaderUDX(udxNode);
        }

        return info;
    }
    // Add these methods to the XmlParserService class:

    private OrderPartiesReference ParseOrderPartiesReference(XmlNode orderPartiesRefNode)
    {
        var reference = new OrderPartiesReference();

        // Parse BUYER_IDREF
        var buyerNode = orderPartiesRefNode.SelectSingleNode("*[local-name()='BUYER_IDREF']");
        if (buyerNode != null)
        {
            reference.BuyerIdRef = new BuyerIdRef()
            {
                Type = buyerNode.Attributes["type"]?.Value,
                Value = buyerNode.InnerText
            };
        }

        // Parse SUPPLIER_IDREF
        var supplierNode = orderPartiesRefNode.SelectSingleNode("*[local-name()='SUPPLIER_IDREF']");
        if (supplierNode != null)
        {
            reference.SupplierIdRef = new SupplierIdRef()
            {
                Type = supplierNode.Attributes["type"]?.Value,
                Value = supplierNode.InnerText
            };
        }

        return reference;
    }

    private HeaderUDX ParseHeaderUDX(XmlNode udxNode)
    {
        var headerUdx = new HeaderUDX
        {
            CustomerType = GetNodeValue(udxNode, "UDX.DG.CUSTOMER_TYPE"),
            DeliveryType = GetNodeValue(udxNode, "UDX.DG.DELIVERY_TYPE"),
            EndCustomerOrderReference = GetNodeValue(udxNode, "UDX.DG.END_CUSTOMER_ORDER_REFERENCE")
        };

        // Parse boolean fields
        var isCollectiveOrder = GetNodeValue(udxNode, "UDX.DG.IS_COLLECTIVE_ORDER");
        if (!string.IsNullOrEmpty(isCollectiveOrder))
        {
            headerUdx.IsCollectiveOrder = bool.Parse(isCollectiveOrder);
        }

        var physicalDeliveryNote = GetNodeValue(udxNode, "UDX.DG.PHYSICAL_DELIVERY_NOTE_REQUIRED");
        if (!string.IsNullOrEmpty(physicalDeliveryNote))
        {
            headerUdx.PhysicalDeliveryNoteRequired = bool.Parse(physicalDeliveryNote);
        }

        // Parse nullable boolean for Saturday delivery
        var saturdayDelivery = GetNodeValue(udxNode, "UDX.DG.SATURDAY_DELIVERY_ALLOWED");
        if (!string.IsNullOrEmpty(saturdayDelivery))
        {
            headerUdx.SaturdayDeliveryAllowed = bool.Parse(saturdayDelivery);
        }

        return headerUdx;
    }

    private ProductDetails ParseProductId(XmlNode productIdNode)
    {
        var productId = new ProductDetails()
        {
            DescriptionShort = GetNodeValue(productIdNode, "DESCRIPTION_SHORT")
        };

        // Parse SUPPLIER_PID
        var supplierPidNode = productIdNode.SelectSingleNode("*[local-name()='SUPPLIER_PID']");
        if (supplierPidNode != null)
        {
            productId.SupplierPid = new SupplierPid()
            {
                Type = supplierPidNode.Attributes["type"]?.Value,
                Value = supplierPidNode.InnerText
            };
        }

        // Parse INTERNATIONAL_PID
        var internationalPidNode = productIdNode.SelectSingleNode("*[local-name()='INTERNATIONAL_PID']");
        if (internationalPidNode != null)
        {
            productId.InternationalPid = new InternationalPid()
            {
                Type = internationalPidNode.Attributes["type"]?.Value,
                Value = internationalPidNode.InnerText
            };
        }

        // Parse BUYER_PID
        var buyerPidNode = productIdNode.SelectSingleNode("*[local-name()='BUYER_PID']");
        if (buyerPidNode != null)
        {
            productId.BuyerPid = new BuyerPid()
            {
                Type = buyerPidNode.Attributes["type"]?.Value,
                Value = buyerPidNode.InnerText
            };
        }

        return productId;
    }

    private ProductPriceFix ParseProductPriceFix(XmlNode priceNode)
    {
        var priceFix = new ProductPriceFix
        {
            PriceAmount = ParseDecimal(GetNodeValue(priceNode, "PRICE_AMOUNT")) ?? 0
        };

        // Parse TAX_DETAILS_FIX
        var taxNode = priceNode.SelectSingleNode("*[local-name()='TAX_DETAILS_FIX']");
        if (taxNode != null)
        {
            priceFix.TaxDetailsFix = new TaxDetailsFix
            {
                TaxAmount = ParseDecimal(GetNodeValue(taxNode, "TAX_AMOUNT")) ?? 0
            };
        }

        return priceFix;
    }

    // Optional: Add parsing for DELIVERY_DATE if needed
    private DeliveryDate ParseDeliveryDate(XmlNode deliveryDateNode)
    {
        if (deliveryDateNode == null) return null;

        var deliveryDate = new DeliveryDate
        {
            Type = deliveryDateNode.Attributes["type"]?.Value
        };

        var startDate = GetNodeValue(deliveryDateNode, "DELIVERY_START_DATE");
        if (!string.IsNullOrEmpty(startDate))
        {
            deliveryDate.DeliveryStartDate = ParseDateTime(startDate) ?? DateTime.MinValue;
        }

        var endDate = GetNodeValue(deliveryDateNode, "DELIVERY_END_DATE");
        if (!string.IsNullOrEmpty(endDate))
        {
            deliveryDate.DeliveryEndDate = ParseDateTime(endDate) ?? DateTime.MinValue;
        }

        return deliveryDate;
    }

    // Optional: Add parsing for CUSTOMER_ORDER_REFERENCE if needed
    private CustomerOrderRefernce ParseCustomerOrderReference(XmlNode customerOrderRefNode)
    {
        if (customerOrderRefNode == null) return null;

        return new CustomerOrderRefernce
        {
            OrderId =ParseInt(GetNodeValue(customerOrderRefNode, "ORDER_ID"))??0 
        };
    }
}