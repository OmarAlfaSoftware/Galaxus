namespace GalaxusIntegration.Application.DTOs.Galaxus
{
    public class Stage2CommercialDto
    {
        public string ProviderKey { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int DeliveryTimeDays { get; set; }
        public int MOQ { get; set; } = 1;
        public int OQS { get; set; } = 1;
        public string Currency { get; set; } = "CHF";
        public decimal VAT_Rate { get; set; } = 7.7m;
    }
}