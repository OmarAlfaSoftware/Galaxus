// src/GalaxusIntegration.Application/Services/Processors/CancelRequestProcessor.cs
using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Application.Strategy_Builder;
using GalaxusIntegration.Core.Entities;
using GalaxusIntegration.Shared.Enum;
using Microsoft.Extensions.Logging;

namespace GalaxusIntegration.Application.Services.Processors;

public class CancelRequestProcessor : IDocumentProcessor
{
    private readonly EntityBuilderStrategy _entityBuilderStrategy;
    private readonly ICancelRequestService _cancelRequestService;
    private readonly ILogger<CancelRequestProcessor> _logger;

    public CancelRequestProcessor(
        EntityBuilderStrategy strategy,
        ICancelRequestService cancelRequestService,
        ILogger<CancelRequestProcessor> logger)
    {
        _entityBuilderStrategy = strategy;
        _cancelRequestService = cancelRequestService;
        _logger = logger;
    }

    public async Task<ProcessingResult> ProcessAsync(UnifiedDocumentDTO document)
    {
        try
        {
            _logger.LogInformation($"Processing cancel request for order: {document.Header?.Info?.OrderId}");

            var strategy = _entityBuilderStrategy.GetStrategy(DocumentType.CANCEL_REQUEST);
            var cancelRequestObject = await strategy.Build(document);
            var cancelRequest = cancelRequestObject as CancelRequest;

            // Process the cancellation request
            var result = await _cancelRequestService.ProcessCancelRequestAsync(cancelRequest);

            // Generate response (will be GCANR - Cancel Confirmation)
            var response = await GenerateCancelResponse(cancelRequest, result);

            return new ProcessingResult
            {
                Success = result.Success,
                Message = result.Success
                    ? $"Cancel request for order {cancelRequest.OrderId} processed successfully"
                    : $"Cancel request for order {cancelRequest.OrderId} rejected",
                Data = response
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing cancel request");
            return new ProcessingResult
            {
                Success = false,
                Message = "Cancel request processing failed",
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public bool CanProcess(DocumentType type)
    {
        return type == DocumentType.CANCEL_REQUEST;
    }

    private async Task<object> GenerateCancelResponse(CancelRequest cancelRequest, CancelProcessingResult result)
    {
        return new
        {
            OrderId = cancelRequest.OrderId,
            CancellationDate = DateTime.UtcNow,
            Items = result.ItemResults.Select(item => new
            {
                ProductId = item.ProductId,
                Accepted = item.Accepted,
                Comment = item.Comment
            })
        };
    }
}

// Service interface
public interface ICancelRequestService
{
    Task<CancelProcessingResult> ProcessCancelRequestAsync(CancelRequest request);
}

public class CancelProcessingResult
{
    public bool Success { get; set; }
    public List<CancelItemResult> ItemResults { get; set; } = new();
}

public class CancelItemResult
{
    public string ProductId { get; set; }
    public bool Accepted { get; set; }
    public string Comment { get; set; }
}
public class CancelRequestService : ICancelRequestService
{
    public async Task<CancelProcessingResult> ProcessCancelRequestAsync(CancelRequest request)
    {
        var res = new CancelProcessingResult();
        res.Success =true;
        res.ItemResults=request.ItemsToCancel.Select(z=>new CancelItemResult() {Accepted=true,Comment="", ProductId=z.ProductId }).ToList();
        
        return res;
    }
}
