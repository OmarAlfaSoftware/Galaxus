// src/GalaxusIntegration.Core/Entities/DispatchNotification.cs
using GalaxusIntegration.Shared.Enum;

namespace GalaxusIntegration.Core.Entities
{

    public class DispatchNotification
    {
        public string DispatchNotificationId { get; set; }
        public DateTime DispatchNotificationDate { get; set; }
        public DateTime GenerationDate { get; set; }
        public DeliveryAddress DeliveryAddress { get; set; }
        public string ShipmentId { get; set; }
        public ShipmentCarrierEnum ShipmentCarrier { get; set; }
        public List<DispatchItem> DispatchItems { get; set; } = new();
        public ProcessingStatus Status { get; set; }
        public List<Party> Parties { get; set; }
    }

    public class DispatchItem
    {
        public string ProductId { get; set; }
        public string InternationalId { get; set; }
        public string BuyerId { get; set; }
        public decimal Quantity { get; set; }
        public string OrderId { get; set; }
        public List<string> SerialNumbers { get; set; } = new();
        public LogisticDetails LogisticDetails { get; set; }
    }

    public class LogisticDetails
    {
        public List <Package> Packages { get; set; }
    }
    public class Package 
    {
        public string PackageId { get; set; }
        public int PackageQuantity { get; set; }
    }


    public enum ShipmentCarrierEnum
    {
        SwissPost,
        PostLogistics,
        DHL,
        DHLFreight,
        UPS,
        FedEx,
        Other
    }
}