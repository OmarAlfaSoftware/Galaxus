using GalaxusIntegration.Application.DTOs.PartialDTOs;
using GalaxusIntegration.Core.Entities;
using System.Xml.Serialization;

namespace GalaxusIntegration.Application.Models;

// This model can handle ORDER, RETURNREGISTRATION, DISPATCHNOTIFICATION, etc.
public class DocumentItemList
{
    [XmlElement("ORDER_ITEM")]
    [XmlElement("RETURNREGISTRATION_ITEM")]
    [XmlElement("DISPATCHNOTIFICATION_ITEM")]
    [XmlElement("ORDERCHANGE_ITEM")]
    public List<DocumentItem> Items { get; set; }
}

public class DocumentItem
{
    [XmlElement("LINE_ITEM_ID")]
    public string LineItemId { get; set; }

    [XmlElement("PRODUCT_ID")]
    public ProductDetails ProductId { get; set; }

    [XmlElement("QUANTITY")]
    public double Quantity { get; set; }

    [XmlElement("ORDER_UNIT", Namespace = "http://www.bmecat.org/bmecat/2005")]
    public string OrderUnit { get; set; }

    // Order-specific
    [XmlElement("PRODUCT_PRICE_FIX")]
    public ProductPriceFix ProductPriceFix { get; set; }

    [XmlElement("PRICE_LINE_AMOUNT")]
    public double? PriceLineAmount { get; set; }

    [XmlElement("DELIVERY_DATE")]
    public DeliveryDate DeliveryDate { get; set; }

    // Return-specific
    [XmlElement("RETURNREASON")]
    public int? ReturnReason { get; set; }

    // Add other item-specific fields as needed
}

public class DocumentSummary
{
    [XmlElement("TOTAL_ITEM_NUM")]
    public int TotalItemNum { get; set; }

    [XmlElement("TOTAL_AMOUNT")]
    public double? TotalAmount { get; set; }
}
// Generic model without multiple XmlElement attributes
public class OpenTransDocument
{
    public string Version { get; set; }
    public string Type { get; set; }
    public string DocumentType { get; set; }
    public DocumentHeader Header { get; set; }
    public DocumentItemList ItemList { get; set; }
    public DocumentSummary Summary { get; set; }
}

public class DocumentHeader
{
    public ControlInfo ControlInfo { get; set; }
    public DocumentInfo Info { get; set; }
}

public class DocumentInfo
{
    public string OrderId { get; set; }
    public DateTime? Date { get; set; }
    public string ReturnRegistrationId { get; set; }
    public string DispatchNotificationId { get; set; }
    public string Language { get; set; }
    public Parties Parties { get; set; }
    public CustomerOrderRefernce CustomerOrderReference { get; set; }
    public OrderPartiesReference OrderPartiesReference { get; set; }
    public string Currency { get; set; }
    public DTOs.PartialDTOs.HeaderUDX HeaderUDX { get; set; }
    public string ShipmentId { get; set; }
    public string TrackingTracingUrl { get; set; }
}

// Rest of your common models...