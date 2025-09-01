using System.Xml.Serialization;

namespace GalaxusIntegration.Application.DTOs.PartialDTOs;

public class OrderItemList
{
    [XmlElement("ORDER_ITEM")]
    public List<OrderItem> Items { get; set; }
}