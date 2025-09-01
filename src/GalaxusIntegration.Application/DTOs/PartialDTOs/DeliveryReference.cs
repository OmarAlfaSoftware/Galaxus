using System;
using System.Xml.Serialization;

namespace GalaxusIntegration.Application.DTOs.PartialDTOs;

public class DeliveryReference
{
    [XmlElement("DELIVERYNOTE_ID")]
    public string DeliveryNoteId { get; set; }

    [XmlElement("DELIVERY_DATE")]
    public DeliveryDate DeliveryDate { get; set; }
}

