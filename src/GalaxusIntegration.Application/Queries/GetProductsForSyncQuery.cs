using MediatR;
using GalaxusIntegration.Application.DTOs.Internal;
using System.Collections.Generic;

namespace GalaxusIntegration.Application.Queries
{
    public class GetProductsForSyncQuery : IRequest<List<ProductDto>>
    {
        public GetProductsForSyncQuery() { }
    }
}