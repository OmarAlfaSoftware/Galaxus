using System.Xml.Serialization;

namespace GalaxusIntegration.Application.DTOs.PartialDTOs;

public class Remark
{
    [XmlText]
    public string Value { get; set; }
}

