// src/GalaxusIntegration.Application/Services/Processors/OrderResponseProcessor.cs
using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Application.Strategy_Builder;
using GalaxusIntegration.Core.Entities;
using GalaxusIntegration.Shared.Enum;
using Microsoft.Extensions.Logging;

namespace GalaxusIntegration.Application.Services.Processors;

public class OrderResponseProcessor : IDocumentProcessor
{
    private readonly EntityBuilderStrategy _entityBuilderStrategy;
    private readonly IOrderResponseService _orderResponseService;
    private readonly ILogger<OrderResponseProcessor> _logger;

    public OrderResponseProcessor(
        EntityBuilderStrategy strategy,
        IOrderResponseService orderResponseService,
        ILogger<OrderResponseProcessor> logger)
    {
        _entityBuilderStrategy = strategy;
        _orderResponseService = orderResponseService;
        _logger = logger;
    }

    public async Task<ProcessingResult> ProcessAsync(UnifiedDocumentDto document)
    {
        try
        {
            _logger.LogInformation($"Processing order response: {document.Header?.Metadata.OrderId}");

            var strategy = _entityBuilderStrategy.GetStrategy(DocumentType.ORDER_RESPONSE);
            var responseObject = await strategy.Build(document);
            var orderResponse = responseObject as OrderResponse;

            // Process the order response
            await _orderResponseService.SendOrderResponseAsync(orderResponse);

            return new ProcessingResult
            {
                Success = true,
                Message = $"Order response for {orderResponse.OrderId} sent successfully",
                Data = orderResponse
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing order response");
            return new ProcessingResult
            {
                Success = false,
                Message = "Order response processing failed",
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public bool CanProcess(DocumentType type)
    {
        return type == DocumentType.ORDER_RESPONSE;
    }
}

// Service interface
public interface IOrderResponseService
{
    Task<OrderResponse> SendOrderResponseAsync(OrderResponse response);
}
public class OrderResponseService : IOrderResponseService
{
    //private readonly IDocumentRepository _repository;
    private readonly ILogger<OrderService> _logger;

    public OrderResponseService(
        //  IDocumentRepository repository,
        ILogger<OrderService> logger)
    {
        //_repository = repository;
        _logger = logger;
    }

    public async Task<OrderResponse> SendOrderResponseAsync(OrderResponse response)
    {
        try
        {
            // Business logic for processing order
            response.Status = ProcessingStatus.Processing;
            //   await _repository.SaveOrderAsync(order);

            // Additional business logic here

            response.Status = ProcessingStatus.Processed;
            response.ProcessedAt = DateTime.UtcNow;
            // await _repository.UpdateDocumentStatusAsync(order.OrderId, ProcessingStatus.Processed);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error processing order {response.OrderId}");
            throw;
        }
    }

    public async Task<Order> GetOrderAsync(string orderId)
    {
        return null; // await _repository.GetOrderAsync(orderId);
    }

    public async Task UpdateOrderStatusAsync(string orderId, ProcessingStatus status)
    {
        //await _repository.UpdateDocumentStatusAsync(orderId, status);
    }
}