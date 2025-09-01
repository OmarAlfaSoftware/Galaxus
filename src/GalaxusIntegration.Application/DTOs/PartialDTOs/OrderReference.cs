using System;
using System.Xml.Serialization;

namespace GalaxusIntegration.Application.DTOs.PartialDTOs;

public class OrderReference
{
    [XmlElement("ORDER_ID")]
    public string OrderId { get; set; }

    [XmlElement("LINE_ITEM_ID")]
    public string LineItemId { get; set; }
}

