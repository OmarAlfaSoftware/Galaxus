using System.Xml.Serialization;

namespace GalaxusIntegration.Application.DTOs.PartialDTOs;

public class LogisticDetails
{
    [XmlElement("TRACKING_ID")]
    public string TrackingId { get; set; }

    [XmlElement("PACKAGE_ID")]
    public string PackageId { get; set; }
}

