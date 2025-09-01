using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using GalaxusIntegration.Application.DTOs.PartialDTOs;
using GalaxusIntegration.Shared.Constants;

namespace GalaxusIntegration.Application.DTOs.Incoming;

[XmlRoot("RETURNREGISTRATION", Namespace = XmlNamespaces.OpenTrans)]
public class ReturnRegistrationDTO
{
    [XmlAttribute("version")]
    public string Version { get; set; }

    [XmlElement("RETURNREGISTRATION_HEADER")]
    public ReturnRegistrationHeader ReturnRegistrationHeader { get; set; }

    [XmlElement("RETURNREGISTRATION_ITEM_LIST")]
    public ReturnRegistrationItemList ReturnRegistrationItemList { get; set; }

    [XmlElement("RETURNREGISTRATION_SUMMARY")]
    public ReturnRegistrationSummary ReturnRegistrationSummary { get; set; }
}

public class ReturnRegistrationHeader
{
    [XmlElement("RETURNREGISTRATION_INFO")]
    public ReturnRegistrationInfo ReturnRegistrationInfo { get; set; }
}

public class ReturnRegistrationInfo
{
    [XmlElement("ORDER_ID")]
    public string OrderId { get; set; }

    [XmlElement("RETURNREGISTRATION_ID")]
    public string ReturnRegistrationId { get; set; }

    [XmlElement("RETURNREGISTRATION_DATE")]
    public DateTime ReturnRegistrationDate { get; set; }

    [XmlElement("LANGUAGE", Namespace = XmlNamespaces.BMECat)]
    public string Language { get; set; }

    [XmlElement("PARTIES")]
    public Parties Parties { get; set; }

    [XmlElement("ORDER_PARTIES_REFERENCE")]
    public OrderPartiesReference OrderPartiesReference { get; set; }

    [XmlElement("SHIPMENT_ID")]
    public string ShipmentId { get; set; }

    [XmlElement("TRACKING_TRACING_URL")]
    public string TrackingTracingUrl { get; set; }
}

public class ReturnRegistrationItemList
{
    [XmlElement("RETURNREGISTRATION_ITEM")]
    public List<ReturnRegistrationItem> Items { get; set; }
}

public class ReturnRegistrationItem
{
    [XmlElement("LINE_ITEM_ID")]
    public string LineItemId { get; set; }

    [XmlElement("PRODUCT_ID")]
    public ProductDetails ProductId { get; set; }

    [XmlElement("QUANTITY")]
    public decimal Quantity { get; set; }

    [XmlElement("ORDER_UNIT", Namespace = XmlNamespaces.BMECat)]
    public string OrderUnit { get; set; }

    [XmlElement("RETURNREASON")]
    public int ReturnReason { get; set; }
}

public class ReturnRegistrationSummary
{
    [XmlElement("TOTAL_ITEM_NUM")]
    public int TotalItemNum { get; set; }
}
