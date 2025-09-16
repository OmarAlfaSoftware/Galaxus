using System.Xml.Serialization;

namespace GalaxusIntegration.Application.DTOs.PartialDTOs;

public class DeliveryDate
{
    [XmlAttribute("type")]
    public string Type { get; set; }

    [XmlElement("DELIVERY_START_DATE")]
    public DateTime? DeliveryStartDate { get; set; }

    [XmlElement("DELIVERY_END_DATE")]
    public DateTime? DeliveryEndDate { get; set; }
}