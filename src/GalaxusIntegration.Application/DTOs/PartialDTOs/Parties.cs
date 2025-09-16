using System.Xml.Serialization;

namespace GalaxusIntegration.Application.DTOs.PartialDTOs;

public class Parties
{
    [XmlElement("PARTY")]
    public List<Party> PartyList { get; set; }
}