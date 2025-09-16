using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using GalaxusIntegration.Application.DTOs.PartialDTOs;
using GalaxusIntegration.Shared.Constants;
using GalaxusIntegration.Application.DTOs.Common;

namespace GalaxusIntegration.Application.DTOs.Outgoing;

[XmlRoot("INVOICE", Namespace = XmlNamespaces.OpenTrans)]
public class InvoiceDTO
{
    [XmlAttribute("version")]
    public string Version { get; set; } = "2.1";

    [XmlElement("INVOICE_HEADER")]
    public InvoiceHeader InvoiceHeader { get; set; }

    [XmlElement("INVOICE_ITEM_LIST")]
    public InvoiceItemList InvoiceItemList { get; set; }

    [XmlElement("INVOICE_SUMMARY")]
    public InvoiceSummary InvoiceSummary { get; set; }
}

public class InvoiceHeader
{
    [XmlElement("CONTROL_INFO")]
    public ControlInfo ControlInfo { get; set; }

    [XmlElement("INVOICE_INFO")]
    public InvoiceInfo InvoiceInfo { get; set; }
}

public class InvoiceInfo
{
    [XmlElement("INVOICE_ID")]
    public string InvoiceId { get; set; }

    [XmlElement("INVOICE_DATE")]
    public DateTime InvoiceDate { get; set; }

    [XmlElement("DELIVERYNOTE_ID")]
    public string DeliveryNoteId { get; set; }

    [XmlElement("DELIVERY_DATE")]
    public DeliveryDate DeliveryDate { get; set; }

    [XmlElement("PARTIES")]
    public Parties Parties { get; set; }

    [XmlElement("REMARKS")]
    public List<Remark> Remarks { get; set; }

    [XmlElement("CURRENCY", Namespace = XmlNamespaces.BMECat)]
    public string Currency { get; set; }

    [XmlElement("ORDER_HISTORY")]
    public OrderHistory OrderHistory { get; set; }
}

public class InvoiceItemList
{
    [XmlElement("INVOICE_ITEM")]
    public List<InvoiceItem> Items { get; set; }
}

public class InvoiceItem : BaseItem
{
    [XmlElement("PRODUCT_PRICE_FIX")]
    public ProductPriceFix ProductPriceFix { get; set; }

    [XmlElement("PRICE_LINE_AMOUNT")]
    public decimal PriceLineAmount { get; set; }

    [XmlElement("ORDER_REFERENCE")]
    public OrderReference OrderReference { get; set; }

    [XmlElement("DELIVERY_REFERENCE")]
    public DeliveryReference DeliveryReference { get; set; }
}

public class InvoiceSummary
{
    [XmlElement("NET_VALUE_GOODS")]
    public decimal NetValueGoods { get; set; }

    [XmlElement("TOTAL_AMOUNT")]
    public decimal TotalAmount { get; set; }

    [XmlElement("ALLOW_OR_CHARGES_FIX")]
    public AllowOrChargesFix AllowOrChargesFix { get; set; }

    [XmlElement("ALLOW_OR_CHARGES_TOTAL_AMOUNT")]
    public decimal? AllowOrChargesTotalAmount { get; set; }

    [XmlElement("TOTAL_TAX")]
    public TotalTax TotalTax { get; set; }
}
