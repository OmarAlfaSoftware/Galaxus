using MediatR;
using GalaxusIntegration.Application.DTOs.Galaxus;

namespace GalaxusIntegration.Application.Commands.Orders
{
    public class SendOrderResponseCommand : IRequest<bool>
    {
        public OrderResponseDto Response { get; set; }
        public SendOrderResponseCommand(OrderResponseDto response)
        {
            Response = response;
        }
    }
}