using GalaxusIntegration.Shared.Enum;

namespace GalaxusIntegration.Core.Entities;

public class SupplierCancelNotification : BaseDocument
{
    public DateTime? GenerationDate { get; set; }
    public string? OrderId { get; set; }
    public DateTime? SupplierCancelNotificationDate { get; set; }
    public List<SupplierCancelItem>? Items { get; set; } = new();
}

public class SupplierCancelItem
{
    public string? SupplierId { get; set; }
    public string? InternationalId { get; set; }
    public string? BuyerId { get; set; }
    public decimal? Quantity { get; set; }
}