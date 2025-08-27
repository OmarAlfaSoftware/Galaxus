namespace GalaxusIntegration.Application.Services
{
    public interface IOrderProcessingService
    {
        Task<bool> ProcessIncomingOrderAsync(string orderXml);
        Task<bool> SendOrderResponseAsync(string responseXml);
        Task<bool> CreateDispatchNotificationAsync(string orderId, string trackingNumber);
        Task<bool> GenerateInvoiceAsync(string orderId, string invoiceId);
        Task<bool> HandleCancellationAsync(string orderId, string reason);
    }
}