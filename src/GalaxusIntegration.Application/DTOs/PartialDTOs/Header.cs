using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GalaxusIntegration.Application.DTOs.PartialDTOs
{
    public class Header
    {
        [XmlElement("CONTROL_INFO")]
        public ControlInfo ControlInfo { get; set; }

        [XmlElement("ORDER_INFO")]
        public OrderInfo OrderInfo { get; set; }
    }
}
