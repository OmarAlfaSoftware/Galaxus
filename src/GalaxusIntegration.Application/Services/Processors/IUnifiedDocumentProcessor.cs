using System.Collections.Generic;
using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Shared.Enum;

namespace GalaxusIntegration.Application.Services.Processors;

public interface IUnifiedDocumentProcessor
{
    Task<ProcessingResult> ProcessAsync(UnifiedDocumentDto document);
    bool CanProcess(DocumentType type);
}
