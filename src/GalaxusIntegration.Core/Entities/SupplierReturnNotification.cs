using GalaxusIntegration.Shared.Enum;

namespace GalaxusIntegration.Core.Entities;

public class SupplierReturnNotification : BaseDocument
{
    public DateTime? GenerationDate { get; set; }
    public string? OrderId { get; set; }
    public DateTime? SupplierReturnNotificationDate { get; set; }
    public List<SupplierReturnItem>? Items { get; set; } = new();
}

public class SupplierReturnItem
{
    public string? SupplierId { get; set; }
    public string? InternationalId { get; set; }
    public string? BuyerId { get; set; }
    public decimal? Quantity { get; set; }
    public bool? RequestAccepted { get; set; }
    public string? ResponseComment { get; set; }
}