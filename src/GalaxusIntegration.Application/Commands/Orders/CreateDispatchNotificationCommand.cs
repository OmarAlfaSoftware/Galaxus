using MediatR;

namespace GalaxusIntegration.Application.Commands.Orders
{
    public class CreateDispatchNotificationCommand : IRequest<bool>
    {
        public string OrderId { get; set; }
        public string TrackingNumber { get; set; }
        public CreateDispatchNotificationCommand(string orderId, string trackingNumber)
        {
            OrderId = orderId;
            TrackingNumber = trackingNumber;
        }
    }
}