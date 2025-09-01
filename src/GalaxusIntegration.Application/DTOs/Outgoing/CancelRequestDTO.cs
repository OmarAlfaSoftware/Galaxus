using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using GalaxusIntegration.Application.DTOs.PartialDTOs;
using GalaxusIntegration.Shared.Constants;

namespace GalaxusIntegration.Application.DTOs.Outgoing;

[XmlRoot("CANCELREQUEST", Namespace = XmlNamespaces.OpenTrans)]
public class CancelRequestDTO
{
    [XmlAttribute("version")]
    public string Version { get; set; } = "2.1";

    [XmlElement("CANCELREQUEST_HEADER")]
    public CancelRequestHeader CancelRequestHeader { get; set; }

    [XmlElement("CANCELREQUEST_ITEM_LIST")]
    public CancelRequestItemList CancelRequestItemList { get; set; }

    [XmlElement("CANCELREQUEST_SUMMARY")]
    public CancelRequestSummary CancelRequestSummary { get; set; }
}

public class CancelRequestHeader
{
    [XmlElement("CANCELREQUEST_INFO")]
    public CancelRequestInfo CancelRequestInfo { get; set; }
}

public class CancelRequestInfo
{
    [XmlElement("ORDER_ID")]
    public string OrderId { get; set; }

    [XmlElement("CANCELREQUEST_DATE")]
    public DateTime CancelRequestDate { get; set; }

    [XmlElement("LANGUAGE", Namespace = XmlNamespaces.BMECat)]
    public string Language { get; set; }

    [XmlElement("PARTIES")]
    public Parties Parties { get; set; }

    [XmlElement("ORDER_PARTIES_REFERENCE")]
    public OrderPartiesReference OrderPartiesReference { get; set; }
}

public class CancelRequestItemList
{
    [XmlElement("CANCELREQUEST_ITEM")]
    public List<CancelRequestItem> Items { get; set; }
}

public class CancelRequestItem
{
    [XmlElement("LINE_ITEM_ID")]
    public string LineItemId { get; set; }

    [XmlElement("PRODUCT_ID")]
    public ProductDetails ProductId { get; set; }

    [XmlElement("QUANTITY")]
    public decimal Quantity { get; set; }

    [XmlElement("ORDER_UNIT", Namespace = XmlNamespaces.BMECat)]
    public string OrderUnit { get; set; }
}

public class CancelRequestSummary
{
    [XmlElement("TOTAL_ITEM_NUM")]
    public int TotalItemNum { get; set; }
}
