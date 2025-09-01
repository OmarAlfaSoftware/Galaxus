using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GalaxusIntegration.Application.DTOs.PartialDTOs
{
    public class Address
    {
        [XmlElement("NAME", Namespace = "http://www.bmecat.org/bmecat/2005")]
        public string? Name { get; set; }

        [XmlElement("NAME2", Namespace = "http://www.bmecat.org/bmecat/2005")]
        public string? Name2 { get; set; }

        [XmlElement("NAME3", Namespace = "http://www.bmecat.org/bmecat/2005")]
        public string? Name3 { get; set; }

        [XmlElement("DEPARTMENT", Namespace = "http://www.bmecat.org/bmecat/2005")]
        public string? Department { get; set; }

        [XmlElement("STREET", Namespace = "http://www.bmecat.org/bmecat/2005")]
        public string Street { get; set; }

        [XmlElement("ZIP", Namespace = "http://www.bmecat.org/bmecat/2005")]
        public string Zip { get; set; }

        [XmlElement("BOXNO", Namespace = "http://www.bmecat.org/bmecat/2005")]
        public string? BoxNo { get; set; }

        [XmlElement("CITY", Namespace = "http://www.bmecat.org/bmecat/2005")]
        public string City { get; set; }

        [XmlElement("COUNTRY", Namespace = "http://www.bmecat.org/bmecat/2005")]
        public string Country { get; set; }

        [XmlElement("COUNTRY_CODED", Namespace = "http://www.bmecat.org/bmecat/2005")]
        public string CountryCoded { get; set; }

        [XmlElement("PHONE", Namespace = "http://www.bmecat.org/bmecat/2005")]
        public string? Phone { get; set; }

        [XmlElement("EMAIL", Namespace = "http://www.bmecat.org/bmecat/2005")]
        public string Email { get; set; }

        [XmlElement("VAT_ID", Namespace = "http://www.bmecat.org/bmecat/2005")]
        public string? VatId { get; set; }

        [XmlElement("CONTACT_DETAILS")]
        public ContactDetails? ContactDetails { get; set; }
    }
}
