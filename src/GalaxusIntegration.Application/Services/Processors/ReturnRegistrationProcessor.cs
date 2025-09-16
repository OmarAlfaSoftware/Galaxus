// src/GalaxusIntegration.Application/Services/Processors/ReturnRegistrationProcessor.cs
using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Application.Strategy_Builder;
using GalaxusIntegration.Core.Entities;
using GalaxusIntegration.Shared.Enum;
using Microsoft.Extensions.Logging;

namespace GalaxusIntegration.Application.Services.Processors;

public class ReturnRegistrationProcessor : IDocumentProcessor
{
    private readonly EntityBuilderStrategy _entityBuilderStrategy;
    private readonly IReturnService _returnService;
    private readonly ILogger<ReturnRegistrationProcessor> _logger;

    public ReturnRegistrationProcessor(
        EntityBuilderStrategy strategy,
        IReturnService returnService,
        ILogger<ReturnRegistrationProcessor> logger)
    {
        _entityBuilderStrategy = strategy;
        _returnService = returnService;
        _logger = logger;
    }

    public async Task<ProcessingResult> ProcessAsync(UnifiedDocumentDTO document)
    {
        try
        {
            _logger.LogInformation($"Processing return registration: {document.Header?.Info?.ReturnRegistrationId}");

            var strategy = _entityBuilderStrategy.GetStrategy(DocumentType.RETURN_REGISTRATION);
            var returnRegObject = await strategy.Build(document);
            var returnRegistration = returnRegObject as ReturnRegistration;

            // Process the return registration
            var result = await _returnService.ProcessReturnAsync(returnRegistration);

            // Generate response (will be GSURN - Supplier Return Notification)
            var response = await GenerateReturnResponse(returnRegistration, result);

            return new ProcessingResult
            {
                Success = result.Success,
                Message = result.Success
                    ? $"Return {returnRegistration.ReturnRegistrationId} processed successfully"
                    : $"Return {returnRegistration.ReturnRegistrationId} rejected",
                Data = response
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing return registration");
            return new ProcessingResult
            {
                Success = false,
                Message = "Return registration processing failed",
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public bool CanProcess(DocumentType type)
    {
        return type == DocumentType.RETURN_REGISTRATION;
    }

    private async Task<object> GenerateReturnResponse(ReturnRegistration registration, ReturnProcessingResult result)
    {
        return new
        {
            OrderId = registration.OrderId,
            ReturnDate = DateTime.UtcNow,
            ShipmentId = registration.ShipmentId,
            Items = result.ItemResults.Select(item => new
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Accepted = item.Accepted,
                Comment = item.Comment
            })
        };
    }
}

// Service interface
public interface IReturnService
{
    Task<ReturnProcessingResult> ProcessReturnAsync(ReturnRegistration registration);
}

public class ReturnProcessingResult
{
    public bool Success { get; set; }
    public List<ReturnItemResult> ItemResults { get; set; } = new();
}

public class ReturnItemResult
{
    public string ProductId { get; set; }
    public decimal Quantity { get; set; }
    public bool Accepted { get; set; }
    public string Comment { get; set; }
}
public class ReturnService : IReturnService
{
    public async Task<ReturnProcessingResult> ProcessReturnAsync(ReturnRegistration registration)
    {
        var result= new ReturnProcessingResult();
        result.Success = true;
        result.ItemResults=registration.ReturnItems.Select(z=>new ReturnItemResult() { Accepted=true,Comment="",ProductId=z.ProductId,Quantity=z.Quantity}).ToList();
        return result ;
    }
}
