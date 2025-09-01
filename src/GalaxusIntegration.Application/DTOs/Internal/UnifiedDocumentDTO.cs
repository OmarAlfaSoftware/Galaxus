using System.Collections.Generic;
using GalaxusIntegration.Application.DTOs.PartialDTOs;
using GalaxusIntegration.Shared.Enum;

namespace GalaxusIntegration.Application.DTOs.Internal;

public class UnifiedDocumentDTO
{
    public DocumentType DocumentType { get; set; }
    public string? Version { get; set; }
    public string? Type { get; set; }

    public DocumentHeader Header { get; set; } = new();
    public DocumentItemList? ItemList { get; set; }
    public DocumentSummary? Summary { get; set; }
    public Dictionary<string, object>? ExtendedProperties { get; set; }
}

public class DocumentHeader
{
    public ControlInfo? ControlInfo { get; set; }
    public DocumentInfo? Info { get; set; }
}

public class DocumentInfo
{
    // Common
    public string? DocumentId { get; set; }
    public DateTime? DocumentDate { get; set; }
    public string? Language { get; set; }
    public string? Currency { get; set; }

    // Order specific
    public string? OrderId { get; set; }
    public DateTime? OrderDate { get; set; }

    // Return specific
    public string? ReturnRegistrationId { get; set; }
    public DateTime? ReturnDate { get; set; }

    // Dispatch specific
    public string? DispatchNotificationId { get; set; }
    public string? ShipmentId { get; set; }
    public string? TrackingUrl { get; set; }

    // Complex
    public Parties? Parties { get; set; }
    public OrderPartiesReference? OrderPartiesReference { get; set; }
    public CustomerOrderRefernce? CustomerOrderReference { get; set; }
    public HeaderUDX? HeaderUDX { get; set; }
}

public class DocumentItemList
{
    public List<DocumentItem> Items { get; set; } = new();
}

public class DocumentItem
{
    public string? LineItemId { get; set; }
    public ProductDetails? ProductId { get; set; }
    public decimal? Quantity { get; set; }
    public string? OrderUnit { get; set; }
    public ProductPriceFix? ProductPriceFix { get; set; }
    public decimal? PriceLineAmount { get; set; }
    public int? ReturnReason { get; set; }
    public DeliveryDate? DeliveryDate { get; set; }
}

public class DocumentSummary
{
    public int TotalItemNum { get; set; }
    public decimal? TotalAmount { get; set; }
}

// Supporting classes
public class ControlInfo
{
    public DateTime? GenerationDate { get; set; }
}

public class Parties
{
    public List<Party> PartyList { get; set; } = new();
}

public class Party
{
    public string? PartyRole { get; set; }
    public List<PartyId> PartyIds { get; set; } = new();
    public Address? Address { get; set; }
}

public class PartyId
{
    public string? Type { get; set; }
    public string? Value { get; set; }
}

public class Address
{
    public string? Name { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? Zip { get; set; }
    public string? Country { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
}

public class ProductDetails
{
    public PidWithType? SupplierPid { get; set; }
    public PidWithType? InternationalPid { get; set; }
    public PidWithType? BuyerPid { get; set; }
    public string? DescriptionShort { get; set; }
    public List<string> SerialNumbers { get; set; } = new();
}

public class PidWithType
{
    public string? Type { get; set; }
    public string? Value { get; set; }
}

public class ProductPriceFix
{
    public decimal Amount { get; set; }
    public string? Currency { get; set; }
}

public class DeliveryDate
{
    public DateTime? Date { get; set; }
    public string? Type { get; set; }
}

public class OrderPartiesReference
{
    public string? BuyerIdRef { get; set; }
    public string? SupplierIdRef { get; set; }
}

public class CustomerOrderRefernce
{
    public string? OrderId { get; set; }
    public string? OrderItemId { get; set; }
}

public class HeaderUDX
{
    public string? CustomerType { get; set; }
    public string? DeliveryType { get; set; }
    public bool? SaturdayDeliveryAllowed { get; set; }
    public bool IsCollectiveOrder { get; set; }
    public string? EndCustomerOrderReference { get; set; }
    public bool PhysicalDeliveryNoteRequired { get; set; }
}
