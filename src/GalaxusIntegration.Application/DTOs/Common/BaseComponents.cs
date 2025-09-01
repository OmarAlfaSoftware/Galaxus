using System.Xml.Serialization;
using GalaxusIntegration.Application.DTOs.PartialDTOs;
using GalaxusIntegration.Shared.Constants;

namespace GalaxusIntegration.Application.DTOs.Common;

// Common base class for document line items
public abstract class BaseItem
{
    [XmlElement("LINE_ITEM_ID")]
    public string LineItemId { get; set; }

    [XmlElement("PRODUCT_ID")]
    public ProductDetails ProductId { get; set; }

    [XmlElement("QUANTITY")]
    public decimal Quantity { get; set; }
}

