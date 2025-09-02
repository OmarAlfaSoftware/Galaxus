using GalaxusIntegration.Shared.Enum;

namespace GalaxusIntegration.Core.Entities;

public class Order : BaseDocument
{
    public DateTime? GenerationDate { get; set; }
    public string? OrderId { get; set; }
    public string? CustOrderId { get; set; }

    public DateTime? OrderDate { get; set; }
    public string? Language { get; set; }
    public string? BuyerId { get; set; }
    public string? SupplierId { get; set; }
    public decimal? TotalAmount { get; set; }
    public string? Currency { get; set; }
    public HeaderUDX? HeaderUDX { get; set; }
    public List<Party>? Parties { get; set; } = new();
    public List<OrderItem>? Items { get; set; } = new();
    public DeliveryInfo? DeliveryInfo { get; set; }

}
public class Party
{
    public List<PartyHeader>? PartyHeaders { get; set; }
    public PartyData? PartyData { get; set; }
}

public class PartyHeader
{
    public string? PartyId { get; set; }
    public string? PartyRole { get; set; }
}
public class PartyData
{
    public string? Name { get; set; }
    public string? Name2 { get; set; }
    public string? Name3 { get; set; }
    public string? Department { get; set; }
    public string? Title { get; set; }
    public string? FirstName { get; set; }
    public string? ContactName { get; set; }
    public string? Street { get; set; }
    public string? Zip { get; set; }
    public string? BoxNo { get; set; }
    public string? City { get; set; }
    public string? CountryCode { get; set; }
    public string? Country { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? VatId { get; set; }
}

public class HeaderUDX
{
    public string? CustomerType { get; set; }
    public string? DeliveryType { get; set; }
    public bool? IsSaturdayAllowed { get; set; }
    public bool? IsCollectiveOrder { get; set; }
    public string? CustomerOrderRef { get; set; }
    public bool? PhysicalDeliveryNote { get; set; }
}
public class OrderItem
{
    public string? LineItemId { get; set; }
    public string? SupplierId { get; set; }
    public string? InternationalId{ get; set; }
    public string? BuyerId{ get; set; }
    public string? Description { get; set; }
    public decimal? Quantity { get; set; }
    public string? Unit { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? TotalPrice { get; set; }
    
}

public class DeliveryInfo
{
 public DateTime? StartDate { get; set; }
 public DateTime? EndDate { get; set; }
}
