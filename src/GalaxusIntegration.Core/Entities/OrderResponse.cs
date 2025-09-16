
namespace GalaxusIntegration.Core.Entities
{
    public class OrderResponse :BaseDocument
    {
        public string? OrderId { get; set; }
        public DateTime OrderResponseDate { get; set; }
        public string SupplierOrderId { get; set; }
        public List<OrderResponseItem> ResponseItems { get; set; }
    }

    public class OrderResponseItem
    {
        public string? ProductId { get; set; }
        public string? InternationalId { get; set; }
        public string? BuyerId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime? DeliveryStartDate { get; set; }
        public DateTime? DeliveryEndDate { get; set; }
    }
}
