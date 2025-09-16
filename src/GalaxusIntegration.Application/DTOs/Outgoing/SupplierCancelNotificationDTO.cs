using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using GalaxusIntegration.Application.DTOs.PartialDTOs;
using GalaxusIntegration.Shared.Constants;

namespace GalaxusIntegration.Application.DTOs.Outgoing;

[XmlRoot("SUPPLIERCANCELNOTIFICATION", Namespace = XmlNamespaces.OpenTrans)]
public class SupplierCancelNotificationDTO
{
    [XmlAttribute("version")]
    public string Version { get; set; } = "2.1";

    [XmlElement("SUPPLIERCANCELNOTIFICATION_HEADER")]
    public SupplierCancelNotificationHeader Header { get; set; }

    [XmlElement("SUPPLIERCANCELNOTIFICATION_ITEM_LIST")]
    public SupplierCancelNotificationItemList ItemList { get; set; }
}

public class SupplierCancelNotificationHeader
{
    [XmlElement("SUPPLIERCANCELNOTIFICATION_INFO")]
    public SupplierCancelNotificationInfo Info { get; set; }
}

public class SupplierCancelNotificationInfo
{
    [XmlElement("ORDER_ID")]
    public string OrderId { get; set; }

    [XmlElement("SUPPLIERCANCELNOTIFICATION_DATE")]
    public DateTime? Date { get; set; }
}

public class SupplierCancelNotificationItemList
{
    [XmlElement("SUPPLIERCANCELNOTIFICATION_ITEM")]
    public List<SupplierCancelNotificationItem> Items { get; set; }
}

public class SupplierCancelNotificationItem
{
    [XmlElement("PRODUCT_ID")]
    public ProductDetails ProductId { get; set; }

    [XmlElement("QUANTITY")]
    public decimal Quantity { get; set; }
}
