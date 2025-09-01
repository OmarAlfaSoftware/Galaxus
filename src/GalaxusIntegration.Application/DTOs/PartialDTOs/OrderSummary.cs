using System.Xml.Serialization;

namespace GalaxusIntegration.Application.DTOs.PartialDTOs;

public class OrderSummary
{
    [XmlElement("TOTAL_ITEM_NUM")]
    public int TotalItemNum { get; set; }
    [XmlElement("TOTAL_AMOUNT")]
    public double TotalAmount { get; set; }
}