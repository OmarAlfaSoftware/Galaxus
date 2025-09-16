using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using GalaxusIntegration.Application.DTOs.PartialDTOs;
using GalaxusIntegration.Shared.Constants;

namespace GalaxusIntegration.Application.DTOs.Outgoing;

[XmlRoot("SUPPLIERRETURNNOTIFICATION", Namespace = XmlNamespaces.OpenTrans)]
public class SupplierReturnNotificationDTO
{
    [XmlAttribute("version")]
    public string Version { get; set; } = "2.1";

    [XmlElement("SUPPLIERRETURNNOTIFICATION_HEADER")]
    public SupplierReturnNotificationHeader Header { get; set; }

    [XmlElement("SUPPLIERRETURNNOTIFICATION_ITEM_LIST")]
    public SupplierReturnNotificationItemList ItemList { get; set; }
}

public class SupplierReturnNotificationHeader
{
    [XmlElement("SUPPLIERRETURNNOTIFICATION_INFO")]
    public SupplierReturnNotificationInfo Info { get; set; }
}

public class SupplierReturnNotificationInfo
{
    [XmlElement("ORDER_ID")]
    public string OrderId { get; set; }

    [XmlElement("SUPPLIERRETURNNOTIFICATION_DATE")]
    public DateTime Date { get; set; }
}

public class SupplierReturnNotificationItemList
{
    [XmlElement("SUPPLIERRETURNNOTIFICATION_ITEM")]
    public List<SupplierReturnNotificationItem> Items { get; set; }
}

public class SupplierReturnNotificationItem
{
    [XmlElement("PRODUCT_ID")]
    public ProductDetails ProductId { get; set; }

    [XmlElement("QUANTITY")]
    public decimal Quantity { get; set; }

    [XmlElement("REQUESTACCEPTED")]
    public bool RequestAccepted { get; set; }

    [XmlElement("RESPONSECOMMENT")]
    public string ResponseComment { get; set; }
}
