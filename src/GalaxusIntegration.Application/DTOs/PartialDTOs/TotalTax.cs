using System.Collections.Generic;
using System.Xml.Serialization;

namespace GalaxusIntegration.Application.DTOs.PartialDTOs;

public class TotalTax
{
    [XmlElement("TAX")]
    public List<Tax> Items { get; set; }
}

public class Tax
{
    [XmlElement("TAX_AMOUNT")]
    public decimal Amount { get; set; }
}

