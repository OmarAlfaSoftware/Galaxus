using GalaxusIntegration.Core.Entities;
using System.Xml.Serialization;

namespace GalaxusIntegration.Application.DTOs.PartialDTOs;

public class OrderItem
{
    [XmlElement("LINE_ITEM_ID")]
    public string LineItemId { get; set; }

    [XmlElement("PRODUCT_ID")]
    public ProductDetails ProductId { get; set; }

    [XmlElement("QUANTITY")]
    public decimal Quantity { get; set; }

    [XmlElement("ORDER_UNIT", Namespace = "http://www.bmecat.org/bmecat/2005")]
    public string OrderUnit { get; set; }

    [XmlElement("PRODUCT_PRICE_FIX")]
    public ProductPriceFix ProductPriceFix { get; set; }

    [XmlElement("PRICE_LINE_AMOUNT")]
    public decimal PriceLineAmount { get; set; }

    [XmlElement("DELIVERY_DATE")]
    public DeliveryDate? DeliveryDate { get; set; }
}
