using MediatR;

namespace GalaxusIntegration.Application.Commands.Orders
{
    public class HandleCancellationCommand : IRequest<bool>
    {
        public string OrderId { get; set; }
        public string Reason { get; set; }
        public HandleCancellationCommand(string orderId, string reason)
        {
            OrderId = orderId;
            Reason = reason;
        }
    }
}