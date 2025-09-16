using System;
using System.Xml.Serialization;

namespace GalaxusIntegration.Application.DTOs.PartialDTOs;

public class OrderHistory
{
    [XmlElement("ORDER_ID")]
    public string OrderId { get; set; }

    [XmlElement("ORDER_DATE")]
    public DateTime? OrderDate { get; set; }
    [XmlElement("SUPPLIER_ORDER_ID")]
    public string SupplierOrderId { get; set; }
}

