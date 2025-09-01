using GalaxusIntegration.Shared.Enum;

namespace GalaxusIntegration.Core.Entities;

public class Order : BaseDocument
{
    public string? OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public string? CustomerId { get; set; }
    public string? SupplierId { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Currency { get; set; }
    public List<OrderItem> Items { get; set; } = new();
    public DeliveryInfo? DeliveryInfo { get; set; }
}

public class OrderItem
{
    public string? LineItemId { get; set; }
    public string? ProductId { get; set; }
    public string? Description { get; set; }
    public decimal Quantity { get; set; }
    public string? Unit { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
}

public class DeliveryInfo
{
    public string? RecipientName { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
}
