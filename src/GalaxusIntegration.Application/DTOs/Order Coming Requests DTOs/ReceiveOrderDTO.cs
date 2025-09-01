using System.Xml.Serialization;
using GalaxusIntegration.Application.DTOs.PartialDTOs;
namespace GalaxusIntegration.Application.DTOs.Order_Coming_Requests;
[XmlRoot("ORDER", Namespace = "http://www.opentrans.org/XMLSchema/2.1")]
public class ReceiveOrderDTO
{
    [XmlAttribute("version")]
    public string Version { get; set; }

    [XmlAttribute("type")]
    public string Type { get; set; }

    [XmlElement("ORDER_HEADER")]
    public OrderHeader OrderHeader { get; set; }

    [XmlElement("ORDER_ITEM_LIST")]
    public OrderItemList OrderItemList { get; set; }

    [XmlElement("ORDER_SUMMARY")]
    public OrderSummary OrderSummary { get; set; }
}