using System.Xml.Serialization;

namespace GalaxusIntegration.Application.DTOs.PartialDTOs;

public class ProductPriceFix
{
    [XmlElement("PRICE_AMOUNT", Namespace = "http://www.bmecat.org/bmecat/2005")]
    public decimal PriceAmount { get; set; }

    [XmlElement("TAX_DETAILS_FIX")]
    public TaxDetailsFix TaxDetailsFix { get; set; }
}