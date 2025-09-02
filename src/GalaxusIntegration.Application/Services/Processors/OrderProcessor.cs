using AutoMapper;
using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Application.Strategy_Builder;
using GalaxusIntegration.Core.Entities;
using GalaxusIntegration.Shared.Enum;
using Microsoft.Extensions.Logging;

namespace GalaxusIntegration.Application.Services.Processors;

public class OrderProcessor : IDocumentProcessor
{
    private readonly EntityBuilderStrategy _entityBuilderStrategy;
    private readonly IOrderService _orderService;
    private readonly ILogger<OrderProcessor> _logger;
    
    public OrderProcessor(
        EntityBuilderStrategy strategy,
        IOrderService orderService,
        ILogger<OrderProcessor> logger)
    {
        _entityBuilderStrategy = strategy;
        _orderService = orderService;
        _logger = logger;
    }
    
    public async Task<ProcessingResult> ProcessAsync(UnifiedDocumentDTO document)
    {
        try
        {
            _logger.LogInformation($"Processing order: {document.Header?.Info?.OrderId}");
            var strategy=_entityBuilderStrategy.GetStrategy(0);
            // Map to domain entity
            var orderObject = await  strategy.Build(document);
            
            var order= orderObject as Order;
            // Process business logic
            await _orderService.ProcessOrderAsync(order);
            
            // Generate response
            var response = await GenerateOrderResponse(order);
            
            return new ProcessingResult
            {
                Success = true,
                Message = $"Order {order.OrderId ?? "Unknown"} processed successfully",
                Data = response
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing order");
            return new ProcessingResult
            {
                Success = false,
                Message = "Order processing failed",
                Errors = new List<string> { ex.Message }
            };
        }
    }
    
    public bool CanProcess(DocumentType type)
    {
        return type == DocumentType.ORDER;
    }
    
    private async Task<object> GenerateOrderResponse(Order order)
    {
        // Create order response DTO
        return new
        {
            OrderId = order.OrderId ?? "Unknown",
            Status = "Accepted",
            ProcessedAt = DateTime.UtcNow
        };
    }
}


public interface IOrderService
{
    Task<Order> ProcessOrderAsync(Order order);
    Task<Order> GetOrderAsync(string orderId);
    Task UpdateOrderStatusAsync(string orderId, ProcessingStatus status);
}

public class OrderService : IOrderService
{
    //private readonly IDocumentRepository _repository;
    private readonly ILogger<OrderService> _logger;

    public OrderService(
      //  IDocumentRepository repository,
        ILogger<OrderService> logger)
    {
        //_repository = repository;
        _logger = logger;
    }

    public async Task<Order> ProcessOrderAsync(Order order)
    {
        try
        {
            // Business logic for processing order
            order.Status = ProcessingStatus.Processing;
         //   await _repository.SaveOrderAsync(order);

            // Additional business logic here

            order.Status = ProcessingStatus.Processed;
            order.ProcessedAt = DateTime.UtcNow;
           // await _repository.UpdateDocumentStatusAsync(order.OrderId, ProcessingStatus.Processed);

            return order;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error processing order {order.OrderId}");
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