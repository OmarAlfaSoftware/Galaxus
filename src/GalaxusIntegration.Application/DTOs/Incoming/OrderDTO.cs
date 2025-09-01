using System;
using System.Xml.Serialization;
using GalaxusIntegration.Application.DTOs.PartialDTOs;
using GalaxusIntegration.Shared.Constants;

namespace GalaxusIntegration.Application.DTOs.Incoming;

[XmlRoot("ORDER", Namespace = XmlNamespaces.OpenTrans)]
public class OrderDTO
{
    [XmlAttribute("version")]
    public string Version { get; set; }

    [XmlAttribute("type")]
    public string Type { get; set; }

    [XmlElement("ORDER_HEADER")]
    public Header OrderHeader { get; set; }

    [XmlElement("ORDER_ITEM_LIST")]
    public ItemList OrderItemList { get; set; }

    [XmlElement("ORDER_SUMMARY")]
    public Summary OrderSummary { get; set; }
}
