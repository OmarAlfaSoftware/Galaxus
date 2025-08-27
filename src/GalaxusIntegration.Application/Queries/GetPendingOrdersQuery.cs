using MediatR;
using GalaxusIntegration.Application.DTOs.Internal;
using System.Collections.Generic;

namespace GalaxusIntegration.Application.Queries
{
    public class GetPendingOrdersQuery : IRequest<List<OrderDto>>
    {
        public GetPendingOrdersQuery() { }
    }
}