using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Core.Entities;

namespace GalaxusIntegration.Application.Interfaces;


public interface IEntityBuilder
{
    Task<object> Build(UnifiedDocumentDto document);

}

