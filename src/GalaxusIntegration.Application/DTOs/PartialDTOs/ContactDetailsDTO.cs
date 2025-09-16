using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GalaxusIntegration.Application.DTOs.PartialDTOs
{
    public class ContactDetails
    {
        [XmlElement("TITLE", Namespace = "http://www.bmecat.org/bmecat/2005")]
        public string? Title { get; set; }

        [XmlElement("FIRST_NAME", Namespace = "http://www.bmecat.org/bmecat/2005")]
        public string? FirstName { get; set; }

        [XmlElement("CONTACT_NAME", Namespace = "http://www.bmecat.org/bmecat/2005")]
        public string? ContactName { get; set; }
    }

}
