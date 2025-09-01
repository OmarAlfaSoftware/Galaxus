using System.Xml.Serialization;

namespace GalaxusIntegration.Application.DTOs.PartialDTOs;

public class CustomerOrderRefernce
{
    [XmlElement("ORDER_ID")]
    public int? OrderId { get; set; }
}