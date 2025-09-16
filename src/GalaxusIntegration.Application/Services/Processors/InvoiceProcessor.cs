// src/GalaxusIntegration.Application/Services/Processors/InvoiceProcessor.cs
using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Application.Strategy_Builder;
using GalaxusIntegration.Core.Entities;
using GalaxusIntegration.Shared.Enum;
using Microsoft.Extensions.Logging;

namespace GalaxusIntegration.Application.Services.Processors;

public class InvoiceProcessor : IDocumentProcessor
{
    private readonly EntityBuilderStrategy _entityBuilderStrategy;
    private readonly IInvoiceService _invoiceService;
    private readonly ILogger<InvoiceProcessor> _logger;

    public InvoiceProcessor(
        EntityBuilderStrategy strategy,
        IInvoiceService invoiceService,
        ILogger<InvoiceProcessor> logger)
    {
        _entityBuilderStrategy = strategy;
        _invoiceService = invoiceService;
        _logger = logger;
    }

    public async Task<ProcessingResult> ProcessAsync(UnifiedDocumentDTO document)
    {
        try
        {
            _logger.LogInformation($"Processing invoice: {document.Header?.Info?.InfoId}");

            var strategy = _entityBuilderStrategy.GetStrategy(DocumentType.INVOICE);
            var invoiceObject = await strategy.Build(document);
            var invoice = invoiceObject as Invoice;

            // Validate invoice
            var validationResult = await _invoiceService.ValidateInvoice(invoice);
            if (!validationResult.IsValid)
            {
                return new ProcessingResult
                {
                    Success = false,
                    Message = "Invoice validation failed",
                    Errors = validationResult.Errors
                };
            }

            // Generate PDF if needed
            if (invoice.RequiresPdf)
            {
                var pdfPath = await _invoiceService.GenerateInvoicePdf(invoice);
                invoice.PdfPath = pdfPath;
            }

            // Send invoice to Galaxus
            await _invoiceService.SendInvoiceAsync(invoice);

            return new ProcessingResult
            {
                Success = true,
                Message = $"Invoice {invoice.InvoiceId} processed successfully",
                Data = invoice
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing invoice");
            return new ProcessingResult
            {
                Success = false,
                Message = "Invoice processing failed",
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public bool CanProcess(DocumentType type)
    {
        return type == DocumentType.INVOICE;
    }
}

// Service interface
public interface IInvoiceService
{
    Task<ValidationResult> ValidateInvoice(Invoice invoice);
    Task<string> GenerateInvoicePdf(Invoice invoice);
    Task<InvoiceResult> SendInvoiceAsync(Invoice invoice);
}
public class InvoiceResult 
{
    public Invoice Invoice { get; set; }
    public string InvoicePath { get; set; }
}
public class InvoiceService : IInvoiceService
{
    public async Task<string> GenerateInvoicePdf(Invoice invoice)
    {
        return "This is the pdf path";
    }

    public async Task<InvoiceResult> SendInvoiceAsync(Invoice invoice)
    {
        InvoiceResult result=new();
        result.InvoicePath=await GenerateInvoicePdf(invoice);
        result.Invoice = invoice;
        return result;
    }

    public async Task<ValidationResult> ValidateInvoice(Invoice invoice)
    {
        var validationResult=new ValidationResult();
        validationResult.IsValid=true;
        return validationResult;
    }
}
