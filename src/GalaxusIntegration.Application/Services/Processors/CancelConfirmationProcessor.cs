using AutoMapper;
using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Application.Strategy_Builder;
using GalaxusIntegration.Core.Entities;
using GalaxusIntegration.Shared.Enum;
using Microsoft.Extensions.Logging;

namespace GalaxusIntegration.Application.Services.Processors;

public class CancelConfirmationProcessor : IDocumentProcessor
{
    private readonly EntityBuilderStrategy _entityBuilderStrategy;
    private readonly ICancelConfirmationService _cancelConfirmationService;
    private readonly ILogger<CancelConfirmationProcessor> _logger;

    public CancelConfirmationProcessor(
        EntityBuilderStrategy strategy,
        ICancelConfirmationService cancelConfirmationService,
        ILogger<CancelConfirmationProcessor> logger)
    {
        _entityBuilderStrategy = strategy;
        _cancelConfirmationService = cancelConfirmationService;
        _logger = logger;
    }

    public async Task<ProcessingResult> ProcessAsync(UnifiedDocumentDto document)
    {
        try
        {
            _logger.LogInformation($"Processing cancel confirmation: {document.Header?.Metadata.OrderId}");
            var strategy = _entityBuilderStrategy.GetStrategy(DocumentType.CANCEL_CONFIRMATION);
            // Map to domain entity
            var cancelConfirmationObject = await strategy.Build(document);

            var cancelConfirmation = cancelConfirmationObject as CancelConfirmation;
            // Process business logic
            await _cancelConfirmationService.ProcessCancelConfirmationAsync(cancelConfirmation);

            // Generate response
            var response = await GenerateCancelConfirmationResponse(cancelConfirmation);

            return new ProcessingResult
            {
                Success = true,
                Message = $"Cancel confirmation for order {cancelConfirmation.OrderId ?? "Unknown"} processed successfully",
                Data = response
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing cancel confirmation");
            return new ProcessingResult
            {
                Success = false,
                Message = "Cancel confirmation processing failed",
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public bool CanProcess(DocumentType type)
    {
        return type == DocumentType.CANCEL_CONFIRMATION;
    }

    private async Task<object> GenerateCancelConfirmationResponse(CancelConfirmation cancelConfirmation)
    {
        // Create cancel confirmation response DTO
        var acceptedCount = cancelConfirmation.Items?.Count(i => i.RequestAccepted == true) ?? 0;
        var rejectedCount = cancelConfirmation.Items?.Count(i => i.RequestAccepted == false) ?? 0;

        return new
        {
            OrderId = cancelConfirmation.OrderId ?? "Unknown",
            Status = "Processed",
            AcceptedItems = acceptedCount,
            RejectedItems = rejectedCount,
            ProcessedAt = DateTime.UtcNow
        };
    }
}

public interface ICancelConfirmationService
{
    Task<CancelConfirmation> ProcessCancelConfirmationAsync(CancelConfirmation cancelConfirmation);
    Task<CancelConfirmation> GetCancelConfirmationAsync(string orderId);
    Task UpdateCancelConfirmationStatusAsync(string orderId, ProcessingStatus status);
}

public class CancelConfirmationService : ICancelConfirmationService
{
    //private readonly IDocumentRepository _repository;
    private readonly ILogger<CancelConfirmationService> _logger;

    public CancelConfirmationService(
        //IDocumentRepository repository,
        ILogger<CancelConfirmationService> logger)
    {
        //_repository = repository;
        _logger = logger;
    }

    public async Task<CancelConfirmation> ProcessCancelConfirmationAsync(CancelConfirmation cancelConfirmation)
    {
        try
        {
            // Business logic for processing cancel confirmation
            cancelConfirmation.Status = ProcessingStatus.Processing;
            //await _repository.SaveCancelConfirmationAsync(cancelConfirmation);

            // Additional business logic here
            // - Update order status based on accepted/rejected items
            // - Send notifications
            // - Update inventory if cancellation is rejected

            foreach (var item in cancelConfirmation.Items ?? new List<CancelConfirmationItem>())
            {
                if (!item.RequestAccepted.HasValue || !item.RequestAccepted.Value)
                {
                    // Validate that comment is provided for rejected items
                    if (string.IsNullOrWhiteSpace(item.ResponseComment))
                    {
                        throw new InvalidOperationException($"Response comment is required for rejected item {item.SupplierId}");
                    }
                }
            }

            cancelConfirmation.Status = ProcessingStatus.Processed;
            cancelConfirmation.ProcessedAt = DateTime.UtcNow;
            //await _repository.UpdateDocumentStatusAsync(cancelConfirmation.OrderId, ProcessingStatus.Processed);

            return cancelConfirmation;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error processing cancel confirmation for order {cancelConfirmation.OrderId}");
            throw;
        }
    }

    public async Task<CancelConfirmation> GetCancelConfirmationAsync(string orderId)
    {
        return null; //await _repository.GetCancelConfirmationAsync(orderId);
    }

    public async Task UpdateCancelConfirmationStatusAsync(string orderId, ProcessingStatus status)
    {
        //await _repository.UpdateDocumentStatusAsync(orderId, status);
    }
}