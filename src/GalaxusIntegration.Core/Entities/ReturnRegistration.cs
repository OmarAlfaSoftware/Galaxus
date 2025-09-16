// src/GalaxusIntegration.Core/Entities/ReturnRegistration.cs
using GalaxusIntegration.Shared.Enum;

namespace GalaxusIntegration.Core.Entities
{

    public class ReturnRegistration : BaseDocument
    {
        public string OrderId { get; set; }
        public string ReturnRegistrationId { get; set; }
        public DateTime ReturnRegistrationDate { get; set; }
        public string Language { get; set; }
        public string ShipmentId { get; set; }
        public string TrackingTracingUrl { get; set; }
        public List<Party> Parties { get; set; } = new();
        public string BuyerIdRef { get; set; }
        public string SupplierIdRef { get; set; }
        public List<ReturnItem> ReturnItems { get; set; } = new();
        public int TotalItemNum { get; set; }
        public ProcessingStatus Status { get; set; }
    }

    public class ReturnItem
    {
        public string LineItemId { get; set; }
        public string ProductId { get; set; }
        public string InternationalId { get; set; }
        public string BuyerId { get; set; }
        public decimal Quantity { get; set; }
        public string OrderUnit { get; set; }
        public ReturnReasonEnum ReturnReason { get; set; }
    }

    public enum ReturnReasonEnum
    {
        DontLike = 1,
        WrongSize = 2,
        WrongProduct = 3,
        DeliveryTooLate = 4,
        DoesNotMatchDescription = 5,
        WrongOrder = 6,
        Other = 99
    }
}