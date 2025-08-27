using MediatR;
using System.Collections.Generic;

namespace GalaxusIntegration.Application.Queries
{
    public class GetFailedFilesQuery : IRequest<List<string>>
    {
        public GetFailedFilesQuery() { }
    }
}