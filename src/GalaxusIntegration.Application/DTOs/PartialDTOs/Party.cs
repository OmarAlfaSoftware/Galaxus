using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GalaxusIntegration.Application.DTOs.PartialDTOs
{
    public class Party
    {
        [XmlElement("PARTY_ID", Namespace = "http://www.bmecat.org/bmecat/2005")]
        public List<PartyId> PartyIds { get; set; }

        [XmlElement("PARTY_ROLE")]
        public string PartyRole { get; set; }

        [XmlElement("ADDRESS")]
        public Address Address { get; set; }
    }

}
