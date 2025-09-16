using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GalaxusIntegration.Application.DTOs.PartialDTOs
{
    public class InternationalPid
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
