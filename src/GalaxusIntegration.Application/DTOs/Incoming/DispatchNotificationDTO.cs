using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using GalaxusIntegration.Application.DTOs.PartialDTOs;
using GalaxusIntegration.Shared.Constants;

namespace GalaxusIntegration.Application.DTOs.Incoming;

[XmlRoot("DISPATCHNOTIFICATION", Namespace = XmlNamespaces.OpenTrans)]
public class DispatchNotificationDTO
{
    [XmlAttribute("version")]
    public string Version { get; set; }

    [XmlElement("DISPATCHNOTIFICATION_HEADER")]
    public DispatchNotificationHeader DispatchNotificationHeader { get; set; }

    [XmlElement("DISPATCHNOTIFICATION_ITEM_LIST")]
    public DispatchNotificationItemList DispatchNotificationItemList { get; set; }
}

public class DispatchNotificationHeader
{
    [XmlElement("CONTROL_INFO")]
    public ControlInfo ControlInfo { get; set; }

    [XmlElement("DISPATCHNOTIFICATION_INFO")]
    public DispatchNotificationInfo DispatchNotificationInfo { get; set; }
}

public class DispatchNotificationInfo
{
    [XmlElement("DISPATCHNOTIFICATION_ID")]
    public string DispatchNotificationId { get; set; }

    [XmlElement("DISPATCHNOTIFICATION_DATE")]
    public DateTime DispatchNotificationDate { get; set; }

    [XmlElement("PARTIES")]
    public Parties Parties { get; set; }

    [XmlElement("SHIPMENT_ID")]
    public string ShipmentId { get; set; }

    [XmlElement("SHIPMENT_CARRIER")]
    public string ShipmentCarrier { get; set; }
}

public class DispatchNotificationItemList
{
    [XmlElement("DISPATCHNOTIFICATION_ITEM")]
    public List<DispatchNotificationItem> Items { get; set; }
}

public class DispatchNotificationItem
{
    [XmlElement("PRODUCT_ID")]
    public ProductDetails ProductId { get; set; }

    [XmlElement("QUANTITY")]
    public decimal Quantity { get; set; }

    [XmlElement("ORDER_REFERENCE")]
    public OrderReference OrderReference { get; set; }

    [XmlElement("LOGISTIC_DETAILS")]
    public LogisticDetails LogisticDetails { get; set; }
}
