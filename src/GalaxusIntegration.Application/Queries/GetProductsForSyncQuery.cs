using MediatR;
using GalaxusIntegration.Application.DTOs;
using System.Collections.Generic;

namespace GalaxusIntegration.Application.Queries
{
    public class GetProductsForSyncQuery : IRequest
    {
        public GetProductsForSyncQuery() { }
    }
}