

namespace GalaxusIntegration.Core.Entities
{
    public class Invoice : BaseDocument
    {
        public string? InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime GenerationDate { get; set; }
        public object DeliveryNoteId { get; set; }
        public string Currency { get; set; }
        public List<InvoiceHistoryItem> HistoryItems { get; set; }
        public object DeliveryStartDate { get; set; }
        public object DeliveryEndDate { get; set; }
        public List<Party> Parties { get; set; }
        public string VatId { get; set; }
        public object QrrReference { get; set; }
        public object ScorReference { get; set; }
        public object QrIban { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }
        public double? NetValueGoods { get; set; }
        public decimal TotalAmount { get; set; }
        public List<AllowOrCharges> AllowOrCharges { get; set; }
        public TotalTax TotalTax { get; set; }
        public bool RequiresPdf { get; set; }
        public string PdfPath { get; set; }
    }
    public class InvoiceHistoryItem 
    {
        public string OrderId { get; set; }
        public string SupplierOrderId { get; set; }
    }

    public class TotalTax
    {
        public decimal? TaxRate { get; set; }
        public decimal? TaxAmount { get; set; }
    }

    public class AllowOrCharges
    {
        public string Type { get; set; }
        public string ChargeType { get; set; }
        public double? Amount { get; set; }
      
        
    }

    public class InvoiceItem
    {
        public string? ProductId { get; set; }
        public string? InternationalId { get; set; }
        public string? BuyerId { get; set; }
        public decimal Quantity { get; set; }
        public decimal PriceAmount { get; set; }
        public double? TaxAmount { get; set; }
        public double? TaxRate { get; set; }
        public decimal PriceLineAmount { get; set; }
        public object OrderId { get; set; }
        public object DeliveryNoteId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
