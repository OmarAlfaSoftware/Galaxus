using MediatR;
using GalaxusIntegration.Application.DTOs.Internal;

namespace GalaxusIntegration.Application.Queries
{
    public class GetSyncStatusQuery : IRequest<SyncResultDto>
    {
        public GetSyncStatusQuery() { }
    }
}