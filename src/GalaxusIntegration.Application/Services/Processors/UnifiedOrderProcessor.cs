using System.Threading.Tasks;
using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Shared.Enum;
using Microsoft.Extensions.Logging;

namespace GalaxusIntegration.Application.Services.Processors;

public class UnifiedOrderProcessor : IUnifiedDocumentProcessor
{
    private readonly ILogger<UnifiedOrderProcessor> _logger;

    public UnifiedOrderProcessor(ILogger<UnifiedOrderProcessor> logger)
    {
        _logger = logger;
    }

    public bool CanProcess(DocumentType type) => type == DocumentType.ORDER;

    public Task<ProcessingResult> ProcessAsync(UnifiedDocumentDTO document)
    {
        var orderId = document.Header?.Info?.OrderId;
        var itemCount = document.ItemList?.Items?.Count ?? 0;

        _logger.LogInformation("Processed ORDER {OrderId} with {Count} items", orderId, itemCount);

        return Task.FromResult(new ProcessingResult
        {
            Success = true,
            Message = $"Order {orderId} processed",
            Data = new { orderId, itemCount }
        });
    }
}
