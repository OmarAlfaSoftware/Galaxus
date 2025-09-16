using System.Xml.Serialization;

namespace GalaxusIntegration.Application.DTOs.PartialDTOs;

public class SupplierPid
{
    [XmlAttribute("type")]
    public string Type { get; set; }

    [XmlText]
    public string Value { get; set; }
}