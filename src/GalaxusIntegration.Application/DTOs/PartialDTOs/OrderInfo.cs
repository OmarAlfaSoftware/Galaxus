using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GalaxusIntegration.Application.DTOs.PartialDTOs
{
    public class OrderInfo
    {
        [XmlElement("ORDER_ID")]
        public string OrderId { get; set; }

        [XmlElement("ORDER_DATE")]
        public DateTime OrderDate { get; set; }

        [XmlElement("LANGUAGE", Namespace = "http://www.bmecat.org/bmecat/2005")]
        public string Language { get; set; }

        [XmlElement("PARTIES")]
        public Parties Parties { get; set; }

        [XmlElement("CUSTOMER_ORDER_REFERENCE")]
        public CustomerOrderRefernce? CustomerOrderReference { get; set; }

        [XmlElement("ORDER_PARTIES_REFERENCE")]
        public OrderPartiesReference OrderPartiesReference { get; set; }

        [XmlElement("CURRENCY", Namespace = "http://www.bmecat.org/bmecat/2005")]
        public string Currency { get; set; }

        [XmlElement("HEADER_UDX")]
        public HeaderUDX HeaderUDX { get; set; }
    }

}
