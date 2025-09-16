using System.Collections.Generic;
using GalaxusIntegration.Application.DTOs.PartialDTOs;
using GalaxusIntegration.Shared.Enum;

namespace GalaxusIntegration.Application.DTOs.Internal;

public class UnifiedDocumentDTO
{
    public DocumentType DocumentType { get; set; }
    public string? Version { get; set; }
    public string? Type { get; set; }

    public DocumentHeader? Header { get; set; } = new();
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
    public string? InfoId { get; set; }
    public string? ShipmentCarrier { get; set; }
    public string? TrackingUrl { get; set; }


    // Complex
    public Parties? Parties { get; set; }
    public OrderPartiesReference? OrderPartiesReference { get; set; }
    public CustomerOrderRefernce? CustomerOrderReference { get; set; }
    public HeaderUDX? HeaderUDX { get; set; }
    public  OrderHistory OrderHistory { get; set; }
    public DeliveryDate DeliveryDate { get;  set; }
    public List<Remark> Remarks { get;  set; }
}

public class DocumentItemList
{
    public List<DocumentItem>? Items { get; set; } = new();
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
    public string OrderId { get; set; }
    public string DeliveryNoteId { get; set; }
    public DeliveryDate? DeliveryDate { get; set; }
    public DocumentItemLogisticDetails? LogisticDetails { get; set; }
    
}
public class DocumentItemLogisticDetails
{
    public DocumentItemPackageInfo? PackageInfo { get; set; }
}
public class DocumentItemPackageInfo 
{
    public List<DocumentItemPackage> Packages { get; set; } = new();
}
public class DocumentItemPackage 
{
    public string PackageId { get; set; }
    public int Quantity  { get; set; }

}
public class DocumentSummary
{
    public int TotalItemNum { get; set; }
    public decimal? TotalAmount { get; set; }
    public decimal? TotalPrice { get; set; }
    public SummaryAllowOrCharge AllowOrChargeFix { get; set; }
    public SummaryTotalTax  TotalTax { get; set; }
}
public class SummaryTotalTax
{
    public decimal? TaxRate { get; internal set; }
    public decimal? TaxAmount { get; internal set; }
}
public class SummaryAllowOrCharge 
{
    public List<AllowOrChargeItem> AllowOrChargeItems { get; set; }
    public double? Total { get; set; }
}
public class AllowOrChargeItem 
{
    public string Type { get; set; }
    public string Name { get; set; }
    public double Amount { get; set; }
}



// Supporting classes
public class ControlInfo
{
    public DateTime? GenerationDate { get; set; }
}

public class Parties
{
    public List<DocumentParty> PartyList { get; set; } = new();
}

public class DocumentParty
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
    public string? Name2 { get; set; }
	public string? Name3 { get; set; }
    public string? Department { get; set; }
    public ContactDetails? ContactDetails { get; set; }
	public string? Street { get; set; }
    public string? BoxNo { get; set; }
	public string? City { get; set; }
    public string? Zip { get; set; }
    public string? Country { get; set; }
    public string? CountryCoded { get; set; }
	public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? VatId { get; set; }
}
public class ContactDetails
{
    public string? Title { get; set; }
    public string? FirstName { get; set; }
    public string? ContactName { get; set; }
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
    public TaxDetailsFix TaxDetailsFix { get; set; }
}

public class DeliveryDate
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class OrderPartiesReference
{
    public string? BuyerIdRef { get; set; }
    public string? SupplierIdRef { get; set; }
}

public class CustomerOrderRefernce
{
    public string? OrderId { get; set; }
  
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
