using AutoMapper;
using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Application.Strategy_Builder;
using GalaxusIntegration.Core.Entities;
using GalaxusIntegration.Shared.Enum;
using Microsoft.Extensions.Logging;

namespace GalaxusIntegration.Application.Services.Processors;

public class SupplierReturnNotificationProcessor : IDocumentProcessor
{
    private readonly EntityBuilderStrategy _entityBuilderStrategy;
    private readonly ISupplierReturnService _supplierReturnService;
    private readonly ILogger<SupplierReturnNotificationProcessor> _logger;

    public SupplierReturnNotificationProcessor(
        EntityBuilderStrategy strategy,
        ISupplierReturnService supplierReturnService,
        ILogger<SupplierReturnNotificationProcessor> logger)
    {
        _entityBuilderStrategy = strategy;
        _supplierReturnService = supplierReturnService;
        _logger = logger;
    }

    public async Task<ProcessingResult> ProcessAsync(UnifiedDocumentDto document)
    {
        try
        {
            _logger.LogInformation($"Processing supplier return notification: {document.Header?.Metadata.OrderId}");
            var strategy = _entityBuilderStrategy.GetStrategy(DocumentType.SUPPLIER_RETURN_NOTIFICATION);
            // Map to domain entity
            var supplierReturnObject = await strategy.Build(document);

            var supplierReturn = supplierReturnObject as SupplierReturnNotification;
            // Process business logic
            await _supplierReturnService.ProcessSupplierReturnAsync(supplierReturn);

            // Generate response
            var response = await GenerateSupplierReturnResponse(supplierReturn);

            return new ProcessingResult
            {
                Success = true,
                Message = $"Supplier return notification for order {supplierReturn.OrderId ?? "Unknown"} processed successfully",
                Data = response
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing supplier return notification");
            return new ProcessingResult
            {
                Success = false,
                Message = "Supplier return notification processing failed",
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public bool CanProcess(DocumentType type)
    {
        return type == DocumentType.SUPPLIER_RETURN_NOTIFICATION;
    }

    private async Task<object> GenerateSupplierReturnResponse(SupplierReturnNotification supplierReturn)
    {
        // Create supplier return response DTO
        var acceptedCount = supplierReturn.Items?.Count(i => i.RequestAccepted == true) ?? 0;
        var rejectedCount = supplierReturn.Items?.Count(i => i.RequestAccepted == false) ?? 0;
        var acceptedQuantity = supplierReturn.Items?.Where(i => i.RequestAccepted == true).Sum(i => i.Quantity ?? 0) ?? 0;
        var rejectedQuantity = supplierReturn.Items?.Where(i => i.RequestAccepted == false).Sum(i => i.Quantity ?? 0) ?? 0;

        return new
        {
            OrderId = supplierReturn.OrderId ?? "Unknown",
            Status = "ReturnProcessed",
            AcceptedReturns = acceptedCount,
            RejectedReturns = rejectedCount,
            AcceptedQuantity = acceptedQuantity,
            RejectedQuantity = rejectedQuantity,
            ProcessedAt = DateTime.UtcNow
        };
    }
}

public interface ISupplierReturnService
{
    Task<SupplierReturnNotification> ProcessSupplierReturnAsync(SupplierReturnNotification supplierReturn);
    Task<SupplierReturnNotification> GetSupplierReturnAsync(string orderId);
    Task UpdateSupplierReturnStatusAsync(string orderId, ProcessingStatus status);
}

public class SupplierReturnService : ISupplierReturnService
{
    //private readonly IDocumentRepository _repository;
    private readonly ILogger<SupplierReturnService> _logger;

    public SupplierReturnService(
        //IDocumentRepository repository,
        ILogger<SupplierReturnService> logger)
    {
        //_repository = repository;
        _logger = logger;
    }

    public async Task<SupplierReturnNotification> ProcessSupplierReturnAsync(SupplierReturnNotification supplierReturn)
    {
        try
        {
            // Business logic for processing supplier return
            supplierReturn.Status = ProcessingStatus.Processing;
            //await _repository.SaveSupplierReturnAsync(supplierReturn);

            // Additional business logic here
            foreach (var item in supplierReturn.Items ?? new List<SupplierReturnItem>())
            {
                if (item.RequestAccepted.HasValue && item.RequestAccepted.Value)
                {
                    _logger.LogInformation($"Return accepted for item {item.SupplierId} with quantity {item.Quantity}");
                    // Process accepted return
                    // - Update inventory
                    // - Process refund
                    // - Generate credit note
                }
                else
                {
                    // Validate that comment is provided for rejected returns
                    if (string.IsNullOrWhiteSpace(item.ResponseComment))
                    {
                        throw new InvalidOperationException($"Response comment is required for rejected return {item.SupplierId}");
                    }
                    _logger.LogInformation($"Return rejected for item {item.SupplierId}: {item.ResponseComment}");
                    // Process rejected return
                    // - Notify customer
                    // - Log rejection reason
                }
            }

            supplierReturn.Status = ProcessingStatus.Processed;
            supplierReturn.ProcessedAt = DateTime.UtcNow;
            //await _repository.UpdateDocumentStatusAsync(supplierReturn.OrderId, ProcessingStatus.Processed);

            return supplierReturn;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error processing supplier return notification for order {supplierReturn.OrderId}");
            throw;
        }
    }

    public async Task<SupplierReturnNotification> GetSupplierReturnAsync(string orderId)
    {
        return null; //await _repository.GetSupplierReturnAsync(orderId);
    }

    public async Task UpdateSupplierReturnStatusAsync(string orderId, ProcessingStatus status)
    {
        //await _repository.UpdateDocumentStatusAsync(orderId, status);
    }
}