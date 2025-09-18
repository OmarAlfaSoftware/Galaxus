using GalaxusIntegration.Shared.Enum;

namespace GalaxusIntegration.Core.Entities;

public class CancelConfirmation : BaseDocument
{
    public DateTime? GenerationDate { get; set; }
    public string? OrderId { get; set; }
    public DateTime? CancelConfirmationDate { get; set; }
    public List<CancelConfirmationItem>? Items { get; set; } = new();
}

public class CancelConfirmationItem
{
    public string? SupplierId { get; set; }
    public string? InternationalId { get; set; }
    public string? BuyerId { get; set; }
    public decimal? Quantity { get; set; }
    public bool? RequestAccepted { get; set; }
    public string? ResponseComment { get; set; }
}