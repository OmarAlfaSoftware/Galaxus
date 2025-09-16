using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using GalaxusIntegration.Application.DTOs.PartialDTOs;
using GalaxusIntegration.Shared.Constants;

namespace GalaxusIntegration.Application.DTOs.Outgoing;

[XmlRoot("CANCELCONFIRMATION", Namespace = XmlNamespaces.OpenTrans)]
public class CancelConfirmationDTO
{
    [XmlAttribute("version")]
    public string Version { get; set; } = "2.1";

    [XmlElement("CANCELCONFIRMATION_HEADER")]
    public CancelConfirmationHeader CancelConfirmationHeader { get; set; }

    [XmlElement("CANCELCONFIRMATION_ITEM_LIST")]
    public CancelConfirmationItemList CancelConfirmationItemList { get; set; }
}

public class CancelConfirmationHeader
{
    [XmlElement("CANCELCONFIRMATION_INFO")]
    public CancelConfirmationInfo CancelConfirmationInfo { get; set; }
}

public class CancelConfirmationInfo
{
    [XmlElement("ORDER_ID")]
    public string OrderId { get; set; }

    [XmlElement("CANCELCONFIRMATION_DATE")]
    public DateTime? CancelConfirmationDate { get; set; }
}

public class CancelConfirmationItemList
{
    [XmlElement("CANCELCONFIRMATION_ITEM")]
    public List<CancelConfirmationItem> Items { get; set; }
}

public class CancelConfirmationItem
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
