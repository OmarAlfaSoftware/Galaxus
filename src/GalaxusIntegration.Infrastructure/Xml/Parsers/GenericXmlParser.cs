using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Infrastructure.Xml.Configuration;
using GalaxusIntegration.Shared.Constants;
using GalaxusIntegration.Shared.Enum;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;
using System.Xml.Serialization;

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
 
    public UnifiedDocumentDto Parse(string xmlContent)
    {
        var documentType = IdentifyDocumentType(xmlContent);
        var doc = XDocument.Parse(xmlContent);
        var root = doc.Root;

        var unifiedDoc = new UnifiedDocumentDto
        {
            DocumentType = documentType,
            Version = root.Attribute("version")?.Value,
            SubType = root.Attribute("type")?.Value
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

    private void ParseDocumentByType(XElement root, UnifiedDocumentDto doc, DocumentType type)
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
            header.ControlInfo = new ControlInformation
            {
                GenerationDate = ParseDateTime(controlInfo.Element(XName.Get("GENERATION_DATE", XmlNamespaces.OpenTrans))?.Value)
            };
        }

        // Parse INFO element
        var infoElement = headerElement.Element(XName.Get(typeInfo.InfoElement, XmlNamespaces.OpenTrans));
        if (infoElement != null)
        {
            header.Metadata = ParseDocumentMetadata(infoElement, typeInfo);
        }

        return header;
    }

    private DocumentMetadata ParseDocumentMetadata(XElement infoElement, GalaxusIntegration.Shared.Constants.DocumentTypeInfo typeInfo)
    {
        var metadata = new DocumentMetadata();

        // Parse common fields
        metadata.DocumentId = GetElementValue(infoElement, "DOCUMENT_ID") ?? GetElementValue(infoElement, $"{typeInfo.RootElement.ToUpperInvariant()}_ID");
        metadata.DocumentDate = ParseDateTime(GetElementValue(infoElement, "DOCUMENT_DATE") ?? GetElementValue(infoElement, $"{typeInfo.RootElement.ToUpperInvariant()}_DATE"));
        metadata.OrderId = GetElementValue(infoElement, "ORDER_ID");
        metadata.Language = GetElementValue(infoElement, "LANGUAGE", XmlNamespaces.BMECat);
        metadata.Currency = GetElementValue(infoElement, "CURRENCY", XmlNamespaces.BMECat);

        // Type-specific fields
        switch (typeInfo.RootElement.ToUpperInvariant())
        {
            case "DISPATCHNOTIFICATION":
                metadata.DispatchNotificationId = GetElementValue(infoElement, "DISPATCHNOTIFICATION_ID");
                metadata.DispatchNotificationDate = ParseDateTime(GetElementValue(infoElement, "DISPATCHNOTIFICATION_DATE"));
                metadata.ShipmentId = GetElementValue(infoElement, "SHIPMENT_ID");
                metadata.ShipmentCarrier = GetElementValue(infoElement, "SHIPMENT_CARRIER");
                break;
            case "RETURNREGISTRATION":
                metadata.ReturnRegistrationId = GetElementValue(infoElement, "RETURNREGISTRATION_ID");
                metadata.ReturnRegistrationDate = ParseDateTime(GetElementValue(infoElement, "RETURNREGISTRATION_DATE"));
                metadata.TrackingUrl = GetElementValue(infoElement, "TRACKING_TRACING_URL");
                break;
            case "INVOICE":
                metadata.InvoiceId = GetElementValue(infoElement, "INVOICE_ID");
                metadata.InvoiceDate = ParseDateTime(GetElementValue(infoElement, "INVOICE_DATE"));
                metadata.DeliveryNoteId = GetElementValue(infoElement, "DELIVERYNOTE_ID");
                break;
            case "ORDERRESPONSE":
                metadata.OrderResponseDate = ParseDateTime(GetElementValue(infoElement, "ORDERRESPONSE_DATE"));
                metadata.SupplierOrderId = GetElementValue(infoElement, "SUPPLIER_ORDER_ID");
                break;
            case "CANCELREQUEST":
                metadata.CancelRequestDate = ParseDateTime(GetElementValue(infoElement, "CANCELREQUEST_DATE"));
                break;
            case "CANCELCONFIRMATION":
                metadata.CancelConfirmationDate = ParseDateTime(GetElementValue(infoElement, "CANCELCONFIRMATION_DATE"));
                break;
            case "SUPPLIERCANCELNOTIFICATION":
                metadata.SupplierCancelNotificationDate = ParseDateTime(GetElementValue(infoElement, "SUPPLIERCANCELNOTIFICATION_DATE"));
                break;
            case "SUPPLIERRETURNNOTIFICATION":
                metadata.SupplierReturnNotificationDate = ParseDateTime(GetElementValue(infoElement, "SUPPLIERRETURNNOTIFICATION_DATE"));
                break;
        }

        // Parse delivery date range if present
        var deliveryDateElement = infoElement.Element(XName.Get("DELIVERY_DATE", XmlNamespaces.OpenTrans));
        if (deliveryDateElement != null)
        {
            metadata.DeliveryDateRange = new DeliveryDateRange
            {
                EarliestDate = ParseDateTime(GetElementValue(deliveryDateElement, "DELIVERY_START_DATE")),
                LatestDate = ParseDateTime(GetElementValue(deliveryDateElement, "DELIVERY_END_DATE"))
            };
        }

        // Parse parties
        var partiesElement = infoElement.Element(XName.Get("PARTIES", XmlNamespaces.OpenTrans));
        if (partiesElement != null)
        {
            metadata.Parties = ParseParties(partiesElement);
        }

        // Parse header UDX
        var headerUdx = infoElement.Element(XName.Get("HEADER_UDX", XmlNamespaces.OpenTrans));
        if (headerUdx != null)
        {
            metadata.UserDefinedExtensions = ParseHeaderUserDefinedExtensions(headerUdx);
        }

        // Parse customer order reference
        var customerOrderRef = infoElement.Element(XName.Get("CUSTOMER_ORDER_REFERENCE", XmlNamespaces.OpenTrans));
        if (customerOrderRef != null)
        {
            metadata.CustomerOrderReference = ParseCustomerOrderReference(customerOrderRef);
        }

        // Parse order parties reference
        var orderParties = infoElement.Element(XName.Get("ORDER_PARTIES_REFERENCE", XmlNamespaces.OpenTrans));
        if (orderParties != null)
        {
            metadata.OrderPartyReferences = ParseOrderPartyReferences(orderParties);
        }

        // Parse order history (from Invoice)
        var orderHistoryElement = infoElement.Element(XName.Get("ORDER_HISTORY", XmlNamespaces.OpenTrans));
        if (orderHistoryElement != null)
        {
            metadata.OrderHistory = new List<OrderHistoryItem>
            {
                new OrderHistoryItem
                {
                    OrderId = GetElementValue(orderHistoryElement, "ORDER_ID"),
                    SupplierOrderId = GetElementValue(orderHistoryElement, "SUPPLIER_ORDER_ID")
                }
            };
        }

        // Parse remarks (from Invoice, with type)
        var remarksElements = infoElement.Elements(XName.Get("REMARKS", XmlNamespaces.OpenTrans));
        metadata.Remarks = remarksElements.Select(r => new Remark
        {
            Type = r.Attribute("type")?.Value ?? string.Empty,
            Text = r.Value
        }).ToList();

        return metadata;
    }

    private OrderPartyReferences ParseOrderPartyReferences(XElement orderPartiesRefElement)
    {
        return new OrderPartyReferences
        {
            BuyerReferenceId = GetElementValue(orderPartiesRefElement, "BUYER_IDREF", XmlNamespaces.BMECat),
            SupplierReferenceId = GetElementValue(orderPartiesRefElement, "SUPPLIER_IDREF", XmlNamespaces.BMECat)
        };
    }

    private CustomerOrderReference ParseCustomerOrderReference(XElement customerOrderRefElement)
    {
        return new CustomerOrderReference
        {
            OrderId = GetElementValue(customerOrderRefElement, "ORDER_ID")
        };
    }

    private HeaderUserDefinedExtensions ParseHeaderUserDefinedExtensions(XElement headerUdxElement)
    {
        return new HeaderUserDefinedExtensions
        {
            CustomerType = GetElementValue(headerUdxElement, "UDX.DG.CUSTOMER_TYPE"),
            DeliveryType = GetElementValue(headerUdxElement, "UDX.DG.DELIVERY_TYPE"),
            IsSaturdayDeliveryAllowed = GetElementValue(headerUdxElement, "UDX.DG.SATURDAY_DELIVERY_ALLOWED") == "true",
            IsCollectiveOrder = GetElementValue(headerUdxElement, "UDX.DG.IS_COLLECTIVE_ORDER") == "true",
            EndCustomerOrderReference = GetElementValue(headerUdxElement, "UDX.DG.END_CUSTOMER_ORDER_REFERENCE"),
            IsPhysicalDeliveryNoteRequired = GetElementValue(headerUdxElement, "UDX.DG.PHYSICAL_DELIVERY_NOTE_REQUIRED") == "true"
        };
    }

    private List<Parties> ParseParties(XElement partiesElement)
    {
        var result= new List<Parties>();
        var partiesXML = partiesElement.Elements(XName.Get("PARTY", XmlNamespaces.OpenTrans));
        foreach (var element in partiesXML)
        {

        var parties = new Parties
        {
            PartyList = new List<DocumentParty>(),
            Role = GetElementValue(element, "PARTY_ROLE",XmlNamespaces.OpenTrans),
        };
     

        // Parse multiple PARTY_ID elements
        foreach (var partyIdElement in element.Elements(XName.Get("PARTY_ID", XmlNamespaces.BMECat)))
        {
            parties.PartyList.Add(new DocumentParty
            {
                PartyIdType = partyIdElement.Attribute("type")?.Value,
                PartyIdValue = partyIdElement.Value
            });
        }

        // Parse address
        var addressElement = element.Element(XName.Get("ADDRESS", XmlNamespaces.OpenTrans));
        if (addressElement != null)
        {
            parties.Address = ParseAddress(addressElement);
        }

        result.Add(parties);
        }
        return result;

     
    }

    private Address ParseAddress(XElement addressElement)
    {
        var contact = new ContactDetails();
        var contactElement = addressElement.Element(XName.Get("CONTACT_DETAILS",XmlNamespaces.OpenTrans));
        if(contactElement!=null)
        {
            contact.Title = GetElementValue(contactElement, "TITLE", XmlNamespaces.BMECat);
            contact.FirstName = GetElementValue(contactElement, "FIRST_NAME", XmlNamespaces.BMECat);
            contact.LastName = GetElementValue(contactElement, "CONTACT_NAME", XmlNamespaces.BMECat);
        }
        return new Address
        {
            Name = GetElementValue(addressElement, "NAME", XmlNamespaces.BMECat),
            NameLine2 = GetElementValue(addressElement, "NAME2", XmlNamespaces.BMECat),
            NameLine3 = GetElementValue(addressElement, "NAME3", XmlNamespaces.BMECat),
            Department = GetElementValue(addressElement, "DEPARTMENT", XmlNamespaces.BMECat),
            Street = GetElementValue(addressElement, "STREET", XmlNamespaces.BMECat),
            PoBoxNumber = GetElementValue(addressElement, "BOXNO", XmlNamespaces.BMECat),
            PostalCode = GetElementValue(addressElement, "ZIP", XmlNamespaces.BMECat),
            City = GetElementValue(addressElement, "CITY", XmlNamespaces.BMECat),
            Country = GetElementValue(addressElement, "COUNTRY", XmlNamespaces.BMECat),
            CountryCode = GetElementValue(addressElement, "COUNTRY_CODED", XmlNamespaces.BMECat),
            PhoneNumber = GetElementValue(addressElement, "PHONE", XmlNamespaces.BMECat),
            EmailAddress = GetElementValue(addressElement, "EMAIL", XmlNamespaces.BMECat),
            VatIdentificationNumber = GetElementValue(addressElement, "VAT_ID", XmlNamespaces.BMECat),
            Contact=contact
        };
    }

    // Parse contact details within address if present
    private ContactDetails? ParseContactDetails(XElement addressElement)
    {
        var contactElement = addressElement.Element(XName.Get("CONTACT_DETAILS", XmlNamespaces.BMECat));
        if (contactElement == null) return null;

        return new ContactDetails
        {
            Title = GetElementValue(contactElement, "TITLE", XmlNamespaces.BMECat),
            FirstName = GetElementValue(contactElement, "FIRST_NAME", XmlNamespaces.BMECat),
            LastName = GetElementValue(contactElement, "CONTACT_NAME", XmlNamespaces.BMECat)
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
            LineTotalAmount = ParseDecimal(GetElementValue(itemElement, "PRICE_LINE_AMOUNT")),
            ReturnReasonCode = ParseInt(GetElementValue(itemElement, "RETURNREASON")),
            ReferencedOrderId = GetElementValue(itemElement, "ORDER_ID"), // From ORDER_REFERENCE
            IsRequestAccepted = bool.Parse(GetElementValue(itemElement, "REQUESTACCEPTED") ?? "false"),
            ResponseComment = GetElementValue(itemElement, "RESPONSECOMMENT") ?? string.Empty,
            ShortDescription = GetElementValue(itemElement, "DESCRIPTION_SHORT", XmlNamespaces.BMECat)
        };

        // Parse PRODUCT_ID
        var productIdElement = itemElement.Element(XName.Get("PRODUCT_ID", XmlNamespaces.OpenTrans));
        if (productIdElement != null)
        {
            item.ProductDetails = ParseProductDetails(productIdElement);
        }

        // Parse serial numbers
        var serialElements = productIdElement?.Elements(XName.Get("SERIAL_NUMBER", XmlNamespaces.BMECat)) ?? Enumerable.Empty<XElement>();
        item.SerialNumbers = serialElements.Select(s => s.Value).ToList();

        // Parse LINE_ITEM_PRICE (updated from PRODUCT_PRICE_FIX)
        var priceElement = itemElement.Element(XName.Get("PRODUCT_PRICE_FIX", XmlNamespaces.OpenTrans)); // Keep XML name, but map to new class
        if (priceElement != null)
        {
            item.LineItemPrice = new LineItemPrice
            {
                Amount = ParseDecimal(GetElementValue(priceElement, "PRICE_AMOUNT", XmlNamespaces.BMECat)) ?? 0,
                Currency = GetElementValue(priceElement, "CURRENCY", XmlNamespaces.BMECat),
                TaxDetails = ParseTaxDetails(priceElement.Element(XName.Get("TAX_DETAILS_FIX", XmlNamespaces.OpenTrans)))
            };
        }

        // Parse delivery reference (from Invoice)
        var deliveryRefElement = itemElement.Element(XName.Get("DELIVERY_REFERENCE", XmlNamespaces.OpenTrans));
        if (deliveryRefElement != null)
        {
            item.DeliveryReference = new DeliveryReference
            {
                DeliveryNoteId = GetElementValue(deliveryRefElement, "DELIVERYNOTE_ID"),
                DeliveryDateRange = new DeliveryDateRange
                {
                    EarliestDate = ParseDateTime(GetElementValue(deliveryRefElement, "DELIVERY_START_DATE")),
                    LatestDate = ParseDateTime(GetElementValue(deliveryRefElement, "DELIVERY_END_DATE"))
                }
            };
        }

        // Parse item delivery date range
        var itemDeliveryElement = itemElement.Element(XName.Get("DELIVERY_DATE", XmlNamespaces.OpenTrans));
        if (itemDeliveryElement != null)
        {
            item.ItemDeliveryDateRange = new DeliveryDateRange
            {
                EarliestDate = ParseDateTime(GetElementValue(itemDeliveryElement, "DELIVERY_START_DATE")),
                LatestDate = ParseDateTime(GetElementValue(itemDeliveryElement, "DELIVERY_END_DATE")),
                Type = itemDeliveryElement.Attribute("type")?.Value
            };
        }

        // Parse logistics details (from Dispatch)
        var logisticsElement = itemElement.Element(XName.Get("LOGISTIC_DETAILS", XmlNamespaces.OpenTrans));
        if (logisticsElement != null)
        {
            item.LogisticsDetails = new ItemLogisticsDetails
            {
                PackageInformation = new ItemPackageInformation
                {
                    Packages = logisticsElement.Element(XName.Get("PACKAGE_INFO", XmlNamespaces.OpenTrans))
                        ?.Elements(XName.Get("PACKAGE", XmlNamespaces.OpenTrans))
                        .Select(p => new ItemPackage
                        {
                            PackageId = GetElementValue(p, "PACKAGE_ID"),
                            PackageQuantity = ParseInt(GetElementValue(p, "PACKAGE_ORDER_UNIT_QUANTITY")) ?? 0
                        }).ToList() ?? new List<ItemPackage>()
                }
            };
        }

        return item;
    }

    private ProductDetails ParseProductDetails(XElement productIdElement)
    {
        return new ProductDetails
        {
            SupplierProductId = ParseProductIdentifier(productIdElement, "SUPPLIER_PID"),
            InternationalProductId = ParseProductIdentifier(productIdElement, "INTERNATIONAL_PID"),
            BuyerProductId = ParseProductIdentifier(productIdElement, "BUYER_PID"),
            ShortDescription = GetElementValue(productIdElement, "DESCRIPTION_SHORT", XmlNamespaces.BMECat)
        };
    }

    private ProductIdentifier? ParseProductIdentifier(XElement parent, string elementName)
    {
        var element = parent.Element(XName.Get(elementName, XmlNamespaces.BMECat));
        if (element == null) return null;

        return new ProductIdentifier
        {
            Type = element.Attribute("type")?.Value,
            Value = element.Value
        };
    }

    private TaxDetails? ParseTaxDetails(XElement? taxElement)
    {
        if (taxElement == null) return null;

        return new TaxDetails
        {
            Rate = ParseDecimal(GetElementValue(taxElement, "TAX", XmlNamespaces.BMECat)),
            Amount = ParseDecimal(GetElementValue(taxElement, "TAX_AMOUNT"))
        };
    }

    private DocumentSummary ParseSummary(XElement summaryElement)
    {
        var summary = new DocumentSummary
        {
            TotalItemCount = ParseInt(GetElementValue(summaryElement, "TOTAL_ITEM_NUM")) ?? 0,
            TotalNetAmount = ParseDecimal(GetElementValue(summaryElement, "NET_VALUE_GOODS")),
            TotalGrossAmount = ParseDecimal(GetElementValue(summaryElement, "TOTAL_AMOUNT"))
        };

        // Parse allowances and charges (from Invoice)
        var aocElement = summaryElement.Element(XName.Get("ALLOW_OR_CHARGES_FIX", XmlNamespaces.OpenTrans));
        if (aocElement != null)
        {
            summary.AllowancesAndCharges = new AllowancesAndCharges
            {
                Items = aocElement.Elements(XName.Get("ALLOW_OR_CHARGE", XmlNamespaces.OpenTrans))
                    .Select(aoc => new AllowanceOrChargeItem
                    {
                        Type = GetElementValue(aoc, "ALLOW_OR_CHARGE_TYPE"),
                        Name = GetElementValue(aoc, "ALLOW_OR_CHARGE_TYPE"), // Reuse type as name if no separate
                        Amount = ParseDecimal(GetElementValue(aoc.Element(XName.Get("ALLOW_OR_CHARGE_VALUE", XmlNamespaces.OpenTrans)), "AOC_MONETARY_AMOUNT"))??0m
                    }).ToList(),
                TotalAmount = ParseDecimal(GetElementValue(aocElement, "ALLOW_OR_CHARGES_TOTAL_AMOUNT"))
            };
        }

        // Parse total tax summary (from Invoice)
        var taxElement = summaryElement.Element(XName.Get("TOTAL_TAX", XmlNamespaces.OpenTrans));
        if (taxElement != null)
        {
            summary.TotalTaxSummary = new TotalTaxSummary
            {
                TaxDetailsList = taxElement.Elements(XName.Get("TAX_DETAILS_FIX", XmlNamespaces.OpenTrans))
                    .Select(t => new TaxDetails
                    {
                        Rate = ParseDecimal(GetElementValue(t, "TAX", XmlNamespaces.BMECat)),
                        Amount = ParseDecimal(GetElementValue(t, "TAX_AMOUNT"))
                    }).ToList(),
                OverallTaxRate = ParseDecimal(GetElementValue(taxElement, "TAX_RATE")),
                OverallTaxAmount = ParseDecimal(GetElementValue(taxElement, "TAX_AMOUNT"))
            };
        }

        return summary;
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