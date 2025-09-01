using GalaxusIntegration.Application.Models;
using Microsoft.Extensions.DependencyInjection;

namespace GalaxusIntegration.Application.Factories;

// Interface for document processors
public interface IDocumentProcessor
{
    Task<object> ProcessAsync(OpenTransDocument document);
}

// Factory to get the right processor
public interface IDocumentProcessorFactory
{
    IDocumentProcessor GetProcessor(string documentType);
}

public class DocumentProcessorFactory : IDocumentProcessorFactory
{
    private readonly IServiceProvider _serviceProvider;

    public DocumentProcessorFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IDocumentProcessor GetProcessor(string documentType)
    {
        return documentType switch
        {
            "ORDER" => _serviceProvider.GetService<OrderProcessor>(),
            "RETURNREGISTRATION" => _serviceProvider.GetService<ReturnProcessor>(),
            _ => throw new NotSupportedException($"No processor for {documentType}")
        };
    }
}

// Specific processors for each type
public class OrderProcessor : IDocumentProcessor
{
    public async Task<object> ProcessAsync(OpenTransDocument document)
    {
        // Order-specific logic
        var orderId = document.Header?.Info?.OrderId;
        var itemCount = document.ItemList?.Items?.Count ?? 0;

        // Save to database, send to queue, etc.

        return new
        {
            type = "Order",
            orderId,
            itemCount,
            status = "Received"
        };
    }
}

public class ReturnProcessor : IDocumentProcessor
{
    public async Task<object> ProcessAsync(OpenTransDocument document)
    {
        // Return-specific logic
        var returnId = document.Header?.Info?.ReturnRegistrationId;
        var orderId = document.Header?.Info?.OrderId;
        var shipmentId = document.Header?.Info?.ShipmentId;

        // Process return logic

        return new
        {
            type = "Return",
            returnId,
            orderId,
            shipmentId,
            status = "Registered"
        };
    }
}