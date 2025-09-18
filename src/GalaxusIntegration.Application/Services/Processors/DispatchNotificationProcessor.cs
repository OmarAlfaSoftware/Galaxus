// src/GalaxusIntegration.Application/Services/Processors/DispatchNotificationProcessor.cs
using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Application.Strategy_Builder;
using GalaxusIntegration.Core.Entities;
using GalaxusIntegration.Shared.Enum;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace GalaxusIntegration.Application.Services.Processors;

public class DispatchNotificationProcessor : IDocumentProcessor
{
    private readonly EntityBuilderStrategy _entityBuilderStrategy;
    private readonly IShippingService _shippingService;
    private readonly ILogger<DispatchNotificationProcessor> _logger;

    public DispatchNotificationProcessor(
        EntityBuilderStrategy strategy,
        IShippingService shippingService,
        ILogger<DispatchNotificationProcessor> logger)
    {
        _entityBuilderStrategy = strategy;
        _shippingService = shippingService;
        _logger = logger;
    }

    public async Task<ProcessingResult> ProcessAsync(UnifiedDocumentDto document)
    {
        try
        {
            _logger.LogInformation($"Processing dispatch notification: {document.Header?.Metadata?.DispatchNotificationId}");

            var strategy = _entityBuilderStrategy.GetStrategy(DocumentType.DISPATCH_NOTIFICATION);
            var dispatchObject = await strategy.Build(document);
            var dispatchNotification = dispatchObject as DispatchNotification;

            // Validate shipping information
            var validationResult = await _shippingService.ValidateShippingInfo(dispatchNotification);
            if (!validationResult.IsValid)
            {
                return new ProcessingResult
                {
                    Success = false,
                    Message = "Invalid shipping information",
                    Errors = validationResult.Errors
                };
            }

            // Send dispatch notification to Galaxus
            await _shippingService.SendDispatchNotificationAsync(dispatchNotification);

            return new ProcessingResult
            {
                Success = true,
                Message = $"Dispatch notification {dispatchNotification.DispatchNotificationId} sent successfully",
                Data = dispatchNotification
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing dispatch notification");
            return new ProcessingResult
            {
                Success = false,
                Message = "Dispatch notification processing failed",
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public bool CanProcess(DocumentType type)
    {
        return type == DocumentType.DISPATCH_NOTIFICATION;
    }
}

// Service interface
public interface IShippingService
{
    Task<ValidationResult> ValidateShippingInfo(DispatchNotification notification);
    Task<DispatchNotification> SendDispatchNotificationAsync(DispatchNotification notification);
}

public class ValidationResult
{
    public bool IsValid { get; set; }
    public List<string> Errors { get; set; } = new();
}
public class ShippingService : IShippingService
{
    public async Task<DispatchNotification> SendDispatchNotificationAsync(DispatchNotification notification)
    {
        var result = await ValidateShippingInfo(notification);
        if(result.IsValid)
        {
            return notification;
        }
        throw new Exception(string.Join("\n", result.Errors.ToArray()));
    }

    public async Task<ValidationResult> ValidateShippingInfo(DispatchNotification notification)
    {
       var result= new ValidationResult() { IsValid = true };
        return result;
    }
}
