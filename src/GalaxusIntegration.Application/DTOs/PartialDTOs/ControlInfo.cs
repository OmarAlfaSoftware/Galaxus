using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GalaxusIntegration.Application.DTOs.PartialDTOs
{
    public class ControlInfo
    {
        [XmlElement("GENERATION_DATE")]
        public DateTime GenerationDate { get; set; }
    }
}
