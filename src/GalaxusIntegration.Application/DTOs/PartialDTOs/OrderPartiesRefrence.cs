using System.Xml.Serialization;

namespace GalaxusIntegration.Application.DTOs.PartialDTOs;

public class OrderPartiesReference
{
    [XmlElement("BUYER_IDREF", Namespace = "http://www.bmecat.org/bmecat/2005")]
    public BuyerIdRef BuyerIdRef { get; set; }

    [XmlElement("SUPPLIER_IDREF", Namespace = "http://www.bmecat.org/bmecat/2005")]
    public SupplierIdRef SupplierIdRef { get; set; }
}