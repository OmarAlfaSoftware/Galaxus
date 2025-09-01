using System.Xml.Serialization;

namespace GalaxusIntegration.Application.DTOs.PartialDTOs;

public class TaxDetailsFix
{
    [XmlElement("TAX_AMOUNT")]
    public double TaxAmount { get; set; }
}