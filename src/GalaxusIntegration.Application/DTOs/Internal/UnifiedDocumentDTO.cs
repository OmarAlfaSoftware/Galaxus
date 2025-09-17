using GalaxusIntegration.Shared.Enum;
namespace GalaxusIntegration.Application.DTOs.Internal
{

public class OrderStatusEvent { public DateTime Timestamp { get; set; } public string Status { get; set; } = string.Empty; public string Description { get; set; } = string.Empty; }
public class Remark { public string Type { get; set; } = string.Empty; public string Text { get; set; } = string.Empty; public DateTime? CreatedDate { get; set; } }
public class OrderHistoryItem { public string OrderId { get; set; } = string.Empty; public string? SupplierOrderId { get; set; } }


public class UnifiedDocumentDto
{
    public DocumentType DocumentType { get; set; }
    public string? Version { get; set; }
    public string? SubType { get; set; }

    public DocumentHeader? Header { get; set; } = new();
    public DocumentItemList? ItemList { get; set; } = new();
    public DocumentSummary? Summary { get; set; } = new();
    public Dictionary<string, object>? ExtendedProperties { get; set; } = new();
}

public class DocumentHeader
{
    public ControlInformation? ControlInfo { get; set; } = new();
    public DocumentMetadata? Metadata { get; set; } = new();
}

public class ControlInformation
{
    public DateTime? GenerationDate { get; set; }
}

public class DocumentMetadata
{
    // Shared document identifiers and dates
    public string? DocumentId { get; set; } // Generic, e.g., INVOICE_ID
    public DateTime? DocumentDate { get; set; } // Generic, e.g., INVOICE_DATE

    // Type-specific identifiers
    public string? DispatchNotificationId { get; set; }
    public DateTime? DispatchNotificationDate { get; set; }
    public string? OrderId { get; set; } // Shared across many
    public DateTime? OrderDate { get; set; }
    public DateTime? CancelConfirmationDate { get; set; } // As DateTime? but string for flexibility if needed
    public DateTime? CancelRequestDate { get; set; }
    public string? InvoiceId { get; set; }
    public DateTime? InvoiceDate { get; set; }
    public DateTime? OrderResponseDate { get; set; } // DateTime?
    public DateTime? ReturnRegistrationDate { get; set; }
    public string? ReturnRegistrationId { get; set; }
    public DateTime? SupplierCancelNotificationDate { get; set; }
    public DateTime? SupplierReturnNotificationDate { get; set; }

    // Shared common fields
    public string? Language { get; set; } // dtLANG
    public string? Currency { get; set; } // dtCURRENCIES

    // Dispatch-specific
    public string? ShipmentId { get; set; }
    public string? ShipmentCarrier { get; set; }
    public string? TrackingUrl { get; set; } // From Return, but shared

    // Invoice/others specific
    public string? DeliveryNoteId { get; set; }
    public DeliveryDateRange? DeliveryDateRange { get; set; } = new(); // Shared delivery dates

    // References
    public CustomerOrderReference? CustomerOrderReference { get; set; } = new();
    public OrderPartyReferences? OrderPartyReferences { get; set; } = new();
    public List<OrderHistoryItem>? OrderHistory { get; set; } = new(); // From Invoice, as list for multiples
    public List<Remark>? Remarks { get; set; } = new(); // From Invoice, with Type

    // Parties (shared across all)
    public List<Parties>? Parties { get; set; } = new();

    // UDX from Order (type-specific, but included)
    public HeaderUserDefinedExtensions? UserDefinedExtensions { get; set; } = new();

    // Other
    public string? SupplierOrderId { get; set; } // From OrderResponse, Invoice
}

public class DocumentItemList
{
    public List<DocumentItem>? Items { get; set; } = new();
}

public class DocumentItem
{
    // Shared product identification
    public string? LineItemId { get; set; } // From CancelRequest, Order, Return
    public ProductDetails? ProductDetails { get; set; } = new();
    public List<string>? SerialNumbers { get; set; } = new(); // From Dispatch, as list

    // Shared quantities and units
    public decimal? Quantity { get; set; } // dtNUMBER, but decimal for flexibility
    public string? OrderUnit { get; set; } // dtPUNIT, e.g., "C62"

    // Pricing (from Order, Invoice)
    public LineItemPrice? LineItemPrice { get; set; } = new();
    public decimal? LineTotalAmount { get; set; } // PRICE_LINE_AMOUNT

    // References
    public string? ReferencedOrderId { get; set; } // ORDER_REFERENCE.ORDER_ID

    // Type-specific
    public int? ReturnReasonCode { get; set; } // From ReturnRegistration
    public bool? IsRequestAccepted { get; set; } // From CancelConfirmation, SupplierReturn
    public string? ResponseComment { get; set; } = string.Empty; // From above

    // Delivery reference (from Invoice)
    public DeliveryReference? DeliveryReference { get; set; } = new();

    // Logistics (from Dispatch)
    public ItemLogisticsDetails? LogisticsDetails { get; set; } = new();

    // Other (from Order)
    public string? ShortDescription { get; set; } // DESCRIPTION_SHORT
    public DeliveryDateRange? ItemDeliveryDateRange { get; set; } = new(); // DELIVERY_DATE in items
}

public class ItemLogisticsDetails
{
    public ItemPackageInformation? PackageInformation { get; set; } = new();
}

public class ItemPackageInformation
{
    public List<ItemPackage>? Packages { get; set; } = new();
}

public class ItemPackage
{
    public string PackageId { get; set; } = string.Empty; // SSCC or consignment
    public int PackageQuantity { get; set; } // PACKAGE_ORDER_UNIT_QUANTITY
}

public class DeliveryReference
{
    public string? DeliveryNoteId { get; set; }
    public DeliveryDateRange? DeliveryDateRange { get; set; } = new();
}

public class DocumentSummary
{
    // Shared totals
    public int TotalItemCount { get; set; } // TOTAL_ITEM_NUM
    public decimal? TotalNetAmount { get; set; } // NET_VALUE_GOODS or TOTAL_AMOUNT excl VAT
    public decimal? TotalGrossAmount { get; set; } // TOTAL_AMOUNT incl VAT

    // From Invoice
    public AllowancesAndCharges? AllowancesAndCharges { get; set; } = new();
    public TotalTaxSummary? TotalTaxSummary { get; set; } = new();
}

public class TotalTaxSummary
{
    // Iterated for multiple rates
    public List<TaxDetails>? TaxDetailsList { get; set; } = new(); // TAX_DETAILS_FIX
    public decimal? OverallTaxRate { get; set; }
    public decimal? OverallTaxAmount { get; set; }
}

public class TaxDetails
{
    public decimal? Rate { get; set; } // TAX as decimal (e.g., 0.081 for 8.1%)
    public decimal? Amount { get; set; } // TAX_AMOUNT
}

public class AllowancesAndCharges
{
    public List<AllowanceOrChargeItem>? Items { get; set; } = new();
    public decimal? TotalAmount { get; set; } // ALLOW_OR_CHARGES_TOTAL_AMOUNT
}

public class AllowanceOrChargeItem
{
    public string Type { get; set; } = string.Empty; // e.g., "freight", "express"
    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; } // AOC_MONETARY_AMOUNT
}

// Supporting classes (refined/expanded)
public class Parties
{
    public string? Role { get; set; } // buyer, supplier, delivery, invoice_issuer, marketplace
    public Address? Address { get; set; } = new();
    public List<DocumentParty>? PartyList { get; set; } = new();
}

public class DocumentParty
{
    public string? PartyIdType { get; set; } // e.g., "buyer_specific", "gln"
    public string? PartyIdValue { get; set; } // The ID
}

public class Address
{
    // Name lines
    public string? Name { get; set; } // Company or primary
    public string? NameLine2 { get; set; } // Attention of
    public string? NameLine3 { get; set; } // Building
    public string? Department { get; set; }

    // Contact
    public ContactDetails? Contact { get; set; } = new();

    // Location
    public string? Street { get; set; }
    public string? PoBoxNumber { get; set; } // BOXNO
    public string? PostalCode { get; set; } // ZIP
    public string? City { get; set; }
    public string? Country { get; set; } // Full name, e.g., "Schweiz"
    public string? CountryCode { get; set; } // ISO, e.g., "CH"

    // Comms
    public string? PhoneNumber { get; set; }
    public string? EmailAddress { get; set; }
    public string? VatIdentificationNumber { get; set; } // VAT_ID
}

public class ContactDetails
{
    public string? Title { get; set; } // Salutation
    public string? FirstName { get; set; }
    public string? LastName { get; set; } // CONTACT_NAME
}

public class ProductDetails
{
    public ProductIdentifier? SupplierProductId { get; set; }
    public ProductIdentifier? InternationalProductId { get; set; } // GTIN-14
    public ProductIdentifier? BuyerProductId { get; set; }
    public string? ShortDescription { get; set; }
}

public class ProductIdentifier
{
    public string? Type { get; set; } // Rarely used, but for PartyId
    public string? Value { get; set; }
}

public class LineItemPrice
{
    public decimal Amount { get; set; } // PRICE_AMOUNT, unit excl VAT
    public string? Currency { get; set; }
    public TaxDetails? TaxDetails { get; set; } = new(); // TAX_DETAILS_FIX, with TAX_AMOUNT per unit
}

public class DeliveryDateRange
{
    public DateTime? EarliestDate { get; set; } // START_DATE
    public DateTime? LatestDate { get; set; } // END_DATE, often same
    public string? Type { get; set; } // "optional", "fixed" from Order
}

public class OrderPartyReferences
{
    public string? BuyerReferenceId { get; set; }
    public string? SupplierReferenceId { get; set; }
}

public class CustomerOrderReference
{
    public string? OrderId { get; set; } // End customer ref
}

public class HeaderUserDefinedExtensions
{
    public string? CustomerType { get; set; } // "private_customer", "company"
    public string? DeliveryType { get; set; } // "direct_delivery", "warehouse_delivery"
    public bool? IsSaturdayDeliveryAllowed { get; set; }
    public bool IsCollectiveOrder { get; set; }
    public string? EndCustomerOrderReference { get; set; }
    public bool IsPhysicalDeliveryNoteRequired { get; set; }
}
}