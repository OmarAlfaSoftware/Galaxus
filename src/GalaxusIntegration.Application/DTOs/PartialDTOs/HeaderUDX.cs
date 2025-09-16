using System.Xml.Serialization;

namespace GalaxusIntegration.Application.DTOs.PartialDTOs;

public class HeaderUDX
{
    [XmlElement("UDX.DG.CUSTOMER_TYPE")]
    public string CustomerType { get; set; }

    [XmlElement("UDX.DG.DELIVERY_TYPE")]
    public string DeliveryType { get; set; }

    [XmlElement("UDX.DG.SATURDAY_DELIVERY_ALLOWED")]
    public bool? SaturdayDeliveryAllowed { get; set; }

    [XmlElement("UDX.DG.IS_COLLECTIVE_ORDER")]
    public bool IsCollectiveOrder { get; set; }

    [XmlElement("UDX.DG.END_CUSTOMER_ORDER_REFERENCE")]
    public string? EndCustomerOrderReference { get; set; }

    [XmlElement("UDX.DG.PHYSICAL_DELIVERY_NOTE_REQUIRED")]
    public bool PhysicalDeliveryNoteRequired { get; set; }

    // Handle the bool? serialization
    public bool ShouldSerializeUDX_DG_SATURDAY_DELIVERY_ALLOWED()
    {
        return SaturdayDeliveryAllowed.HasValue;
    }
}