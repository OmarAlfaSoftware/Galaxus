using System.Xml.Serialization;

namespace GalaxusIntegration.Application.DTOs.PartialDTOs;

public class ProductDetails
{
   
        [XmlElement("SUPPLIER_PID", Namespace = "http://www.bmecat.org/bmecat/2005")]
        public SupplierPid SupplierPid { get; set; }

        [XmlElement("INTERNATIONAL_PID", Namespace = "http://www.bmecat.org/bmecat/2005")]
        public InternationalPid InternationalPid { get; set; }

        [XmlElement("BUYER_PID", Namespace = "http://www.bmecat.org/bmecat/2005")]
        public BuyerPid BuyerPid { get; set; }

        [XmlElement("DESCRIPTION_SHORT", Namespace = "http://www.bmecat.org/bmecat/2005")]
        public string DescriptionShort { get; set; }

}