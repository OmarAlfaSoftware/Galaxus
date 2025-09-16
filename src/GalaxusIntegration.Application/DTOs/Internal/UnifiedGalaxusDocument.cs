using System;
using System.Collections.Generic;
using GalaxusIntegration.Application.DTOs.PartialDTOs;
using GalaxusIntegration.Shared.Enum;

namespace GalaxusIntegration.Application.DTOs.Internal;

public class UnifiedGalaxusDocument
{
    public DocumentType DocumentType { get; set; }
    public string? Version { get; set; }
    public string? Type { get; set; }

    // Common header fields
    public DateTime? GenerationDate { get; set; }
    public DateTime? DocumentDate { get; set; }
    public string? Language { get; set; }
    public string? Currency { get; set; }

    // Identifiers
    public string? OrderId { get; set; }
    public string? ReturnRegistrationId { get; set; }
    public string? DispatchNotificationId { get; set; }
    public string? InvoiceId { get; set; }
    public string? SupplierOrderId { get; set; }
    public string? DeliveryNoteId { get; set; }

    // Parties and references
    public List<Party>? Parties { get; set; }
    public OrderPartiesReference? OrderPartiesReference { get; set; }
    public CustomerOrderRefernce? CustomerOrderReference { get; set; }
    public HeaderUDX? HeaderUDX { get; set; }

    // Shipment
    public string? ShipmentId { get; set; }
    public string? TrackingTracingUrl { get; set; }
    public string? ShipmentCarrier { get; set; }

    // Items and summary
    public List<UnifiedItem>? Items { get; set; }
    public int? TotalItemNum { get; set; }
    public decimal? TotalAmount { get; set; }
    public decimal? NetValueGoods { get; set; }
    public AllowOrChargesFix? AllowOrCharges { get; set; }
    public TotalTax? TotalTax { get; set; }

    // Misc
    public List<Remark>? Remarks { get; set; }
    public OrderHistory? OrderHistory { get; set; }
}

public class UnifiedItem
{
    public string? LineItemId { get; set; }
    public ProductDetails? ProductId { get; set; }
    public decimal? Quantity { get; set; }
    public string? OrderUnit { get; set; }
    public ProductPriceFix? ProductPriceFix { get; set; }
    public decimal? PriceLineAmount { get; set; }
    public int? ReturnReason { get; set; }
    public DeliveryDate? DeliveryDate { get; set; }
    public OrderReference? OrderReference { get; set; }
    public LogisticDetails? LogisticDetails { get; set; }
    public DeliveryReference? DeliveryReference { get; set; }
    public bool? RequestAccepted { get; set; }
    public string? ResponseComment { get; set; }
}

