using MediatR;
using GalaxusIntegration.Application.DTOs.Galaxus;

namespace GalaxusIntegration.Application.Commands.Orders
{
    public class ProcessIncomingOrderCommand : IRequest<bool>
    {
        public GalaxusOrderDto Order { get; set; }
        public ProcessIncomingOrderCommand(GalaxusOrderDto order)
        {
            Order = order;
        }
    }
}