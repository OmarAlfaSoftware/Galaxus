using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using GalaxusIntegration.Application.DTOs.PartialDTOs;
using GalaxusIntegration.Shared.Constants;

namespace GalaxusIntegration.Application.DTOs.Outgoing;

[XmlRoot("ORDERRESPONSE", Namespace = XmlNamespaces.OpenTrans)]
public class OrderResponseDTO
{
    [XmlAttribute("version")]
    public string Version { get; set; } = "2.1";

    [XmlAttribute("type")]
    public string Type { get; set; } = "standard";

    [XmlElement("ORDERRESPONSE_HEADER")]
    public OrderResponseHeader OrderResponseHeader { get; set; }

    [XmlElement("ORDERRESPONSE_ITEM_LIST")]
    public OrderResponseItemList OrderResponseItemList { get; set; }
}

public class OrderResponseHeader
{
    [XmlElement("ORDERRESPONSE_INFO")]
    public OrderResponseInfo OrderResponseInfo { get; set; }
}

public class OrderResponseInfo
{
    [XmlElement("ORDER_ID")]
    public string OrderId { get; set; }

    [XmlElement("ORDERRESPONSE_DATE")]
    public DateTime OrderResponseDate { get; set; }

    [XmlElement("SUPPLIER_ORDER_ID")]
    public string SupplierOrderId { get; set; }
}

public class OrderResponseItemList
{
    [XmlElement("ORDERRESPONSE_ITEM")]
    public List<OrderResponseItem> Items { get; set; }
}

public class OrderResponseItem
{
    [XmlElement("PRODUCT_ID")]
    public ProductDetails ProductId { get; set; }

    [XmlElement("QUANTITY")]
    public decimal Quantity { get; set; }

    [XmlElement("DELIVERY_DATE")]
    public DeliveryDate DeliveryDate { get; set; }
}

