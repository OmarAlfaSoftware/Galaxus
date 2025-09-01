using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Shared.Enum;

namespace GalaxusIntegration.Application.Services.Processors;

public interface IDocumentProcessor
{
    Task<ProcessingResult> ProcessAsync(UnifiedDocumentDTO document);
    bool CanProcess(DocumentType type);
}

public class ProcessingResult
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public object? Data { get; set; }
    public List<string> Errors { get; set; } = new();
}
