using System.Collections.Generic;
using System.Xml.Serialization;

namespace GalaxusIntegration.Application.DTOs.PartialDTOs;

public class AllowOrChargesFix
{
    [XmlElement("ALLOW_OR_CHARGE")]
    public List<AllowOrCharge> Items { get; set; }
}

public class AllowOrCharge
{
    [XmlElement("AMOUNT")]
    public decimal Amount { get; set; }
}

