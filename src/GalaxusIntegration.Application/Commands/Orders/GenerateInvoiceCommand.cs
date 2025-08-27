using MediatR;

namespace GalaxusIntegration.Application.Commands.Orders
{
    public class GenerateInvoiceCommand : IRequest<bool>
    {
        public string OrderId { get; set; }
        public string InvoiceId { get; set; }
        public GenerateInvoiceCommand(string orderId, string invoiceId)
        {
            OrderId = orderId;
            InvoiceId = invoiceId;
        }
    }
}