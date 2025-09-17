using AutoMapper;
using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Application.Strategy_Builder;
using GalaxusIntegration.Core.Entities;
using GalaxusIntegration.Shared.Enum;
using Microsoft.Extensions.Logging;

namespace GalaxusIntegration.Application.Services.Processors;

public class SupplierCancelNotificationProcessor : IDocumentProcessor
{
    private readonly EntityBuilderStrategy _entityBuilderStrategy;
    private readonly ISupplierCancelService _supplierCancelService;
    private readonly ILogger<SupplierCancelNotificationProcessor> _logger;

    public SupplierCancelNotificationProcessor(
        EntityBuilderStrategy strategy,
        ISupplierCancelService supplierCancelService,
        ILogger<SupplierCancelNotificationProcessor> logger)
    {
        _entityBuilderStrategy = strategy;
        _supplierCancelService = supplierCancelService;
        _logger = logger;
    }

    public async Task<ProcessingResult> ProcessAsync(UnifiedDocumentDto document)
    {
        try
        {
            _logger.LogInformation($"Processing supplier cancel notification: {document.Header?.Metadata?.OrderId}");
            var strategy = _entityBuilderStrategy.GetStrategy(DocumentType.SUPPLIER_CANCEL_NOTIFICATION);
            // Map to domain entity
            var supplierCancelObject = await strategy.Build(document);

            var supplierCancel = supplierCancelObject as SupplierCancelNotification;
            // Process business logic
            await _supplierCancelService.ProcessSupplierCancelAsync(supplierCancel);

            // Generate response
            var response = await GenerateSupplierCancelResponse(supplierCancel);

            return new ProcessingResult
            {
                Success = true,
                Message = $"Supplier cancel notification for order {supplierCancel.OrderId ?? "Unknown"} processed successfully",
                Data = response
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing supplier cancel notification");
            return new ProcessingResult
            {
                Success = false,
                Message = "Supplier cancel notification processing failed",
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public bool CanProcess(DocumentType type)
    {
        return type == DocumentType.SUPPLIER_CANCEL_NOTIFICATION;
    }

    private async Task<object> GenerateSupplierCancelResponse(SupplierCancelNotification supplierCancel)
    {
        // Create supplier cancel response DTO
        var totalQuantity = supplierCancel.Items?.Sum(i => i.Quantity ?? 0) ?? 0;

        return new
        {
            OrderId = supplierCancel.OrderId ?? "Unknown",
            Status = "Cancelled",
            ItemCount = supplierCancel.Items?.Count ?? 0,
            TotalQuantityCancelled = totalQuantity,
            ProcessedAt = DateTime.UtcNow
        };
    }
}

public interface ISupplierCancelService
{
    Task<SupplierCancelNotification> ProcessSupplierCancelAsync(SupplierCancelNotification supplierCancel);
    Task<SupplierCancelNotification> GetSupplierCancelAsync(string orderId);
    Task UpdateSupplierCancelStatusAsync(string orderId, ProcessingStatus status);
}

public class SupplierCancelService : ISupplierCancelService
{
    //private readonly IDocumentRepository _repository;
    private readonly ILogger<SupplierCancelService> _logger;

    public SupplierCancelService(
        //IDocumentRepository repository,
        ILogger<SupplierCancelService> logger)
    {
        //_repository = repository;
        _logger = logger;
    }

    public async Task<SupplierCancelNotification> ProcessSupplierCancelAsync(SupplierCancelNotification supplierCancel)
    {
        try
        {
            // Business logic for processing supplier cancellation
            supplierCancel.Status = ProcessingStatus.Processing;
            //await _repository.SaveSupplierCancelAsync(supplierCancel);

            // Additional business logic here
            // - Update order status to cancelled
            // - Restock inventory
            // - Send cancellation notification to Galaxus
            // - Update financial records

            foreach (var item in supplierCancel.Items ?? new List<SupplierCancelItem>())
            {
                _logger.LogInformation($"Cancelling item {item.SupplierId} with quantity {item.Quantity}");
                // Process each cancelled item
                // - Return to inventory
                // - Update product availability
            }

            supplierCancel.Status = ProcessingStatus.Processed;
            supplierCancel.ProcessedAt = DateTime.UtcNow;
            //await _repository.UpdateDocumentStatusAsync(supplierCancel.OrderId, ProcessingStatus.Processed);

            return supplierCancel;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error processing supplier cancel notification for order {supplierCancel.OrderId}");
            throw;
        }
    }

    public async Task<SupplierCancelNotification> GetSupplierCancelAsync(string orderId)
    {
        return null; //await _repository.GetSupplierCancelAsync(orderId);
    }

    public async Task UpdateSupplierCancelStatusAsync(string orderId, ProcessingStatus status)
    {
        //await _repository.UpdateDocumentStatusAsync(orderId, status);
    }
}