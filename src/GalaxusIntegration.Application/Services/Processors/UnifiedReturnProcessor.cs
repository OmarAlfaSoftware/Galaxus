using System.Threading.Tasks;
using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Shared.Enum;
using Microsoft.Extensions.Logging;

namespace GalaxusIntegration.Application.Services.Processors;

public class UnifiedReturnProcessor : IUnifiedDocumentProcessor
{
    private readonly ILogger<UnifiedReturnProcessor> _logger;
    public UnifiedReturnProcessor(ILogger<UnifiedReturnProcessor> logger)
    {
        _logger = logger;
    }

    public bool CanProcess(DocumentType type) => type == DocumentType.RETURN_REGISTRATION;

    public Task<ProcessingResult> ProcessAsync(UnifiedDocumentDto document)
    {
        var returnId = document.Header?.Metadata?.ReturnRegistrationId;
        var orderId = document.Header?.Metadata.OrderId;
        var itemCount = document.ItemList?.Items?.Count ?? 0;
        _logger.LogInformation("Processed RETURNREGISTRATION {ReturnId} for order {OrderId}", returnId, orderId);
        return Task.FromResult(new ProcessingResult
        {
            Success = true,
            Message = $"Return {returnId} processed",
            Data = new { returnId, orderId, itemCount }
        });
    }
}
