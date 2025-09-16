//using GalaxusIntegration.Core.Entities;
//using GalaxusIntegration.Domain.Entities;
//using GalaxusIntegration.Domain.Enums;
//using GalaxusIntegration.Shared.Enum;
//using GalaxusIntegration;
//using Microsoft.Extensions.Logging;

//namespace GalaxusIntegration.Application.Interfaces
//{
//public interface IDocumentRepository
//{
//    // Order specific operations
//    Task<Order> SaveOrderAsync(Order order);
//    Task<Order> GetOrderAsync(string orderId);
//    Task<List<Order>> GetOrdersByStatusAsync(ProcessingStatus status);

//    // Generic document operations
//    Task<BaseDocument> SaveDocumentAsync(BaseDocument document);
//    Task<BaseDocument> GetDocumentAsync(string documentId);
//    Task<BaseDocument> GetDocumentByTypeAndIdAsync(DocumentType type, string documentId);
//    Task UpdateDocumentStatusAsync(string documentId, ProcessingStatus status);

//    // Batch operations
//    Task<List<BaseDocument>> GetPendingDocumentsAsync();
//    Task<List<BaseDocument>> GetDocumentsByDateRangeAsync(DateTime startDate, DateTime endDate);

//    // Check operations
//    Task<bool> DocumentExistsAsync(string documentId);
//    Task<bool> OrderExistsAsync(string orderId);
//}

//public class DocumentRepository : IDocumentRepository
//{
//  //  private readonly ApplicationDbContext _context;
//    private readonly ILogger<DocumentRepository> _logger;

//    public DocumentRepository(
//        ApplicationDbContext context,
//        ILogger<DocumentRepository> logger)
//    {
//        _context = context;
//        _logger = logger;
//    }

//    // Order specific operations
//    public async Task<Order> SaveOrderAsync(Order order)
//    {
//        try
//        {
//            var existingOrder = await _context.Orders
//                .FirstOrDefaultAsync(o => o.OrderId == order.OrderId);

//            if (existingOrder != null)
//            {
//                // Update existing order
//                _context.Entry(existingOrder).CurrentValues.SetValues(order);
//                existingOrder.Items = order.Items; // Update items collection
//                existingOrder.DeliveryInfo = order.DeliveryInfo;
//            }
//            else
//            {
//                // Add new order
//                _context.Orders.Add(order);
//            }

//            await _context.SaveChangesAsync();
//            _logger.LogInformation($"Order {order.OrderId} saved successfully");
//            return order;
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, $"Error saving order {order.OrderId}");
//            throw;
//        }
//    }

//    public async Task<Order> GetOrderAsync(string orderId)
//    {
//        return await _context.Orders
//            .Include(o => o.Items)
//            .Include(o => o.DeliveryInfo)
//            .FirstOrDefaultAsync(o => o.OrderId == orderId);
//    }

//    public async Task<List<Order>> GetOrdersByStatusAsync(ProcessingStatus status)
//    {
//        return await _context.Orders
//            .Where(o => o.Status == status)
//            .Include(o => o.Items)
//            .ToListAsync();
//    }

//    // Generic document operations
//    public async Task<BaseDocument> SaveDocumentAsync(BaseDocument document)
//    {
//        try
//        {
//            var existingDoc = await _context.Documents
//                .FirstOrDefaultAsync(d => d.DocumentId == document.DocumentId
//                                          && d.Type == document.Type);

//            if (existingDoc != null)
//            {
//                // Update existing document
//                _context.Entry(existingDoc).CurrentValues.SetValues(document);
//            }
//            else
//            {
//                // Add new document
//                _context.Documents.Add(document);
//            }

//            await _context.SaveChangesAsync();
//            _logger.LogInformation($"Document {document.DocumentId} of type {document.Type} saved");
//            return document;
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, $"Error saving document {document.DocumentId}");
//            throw;
//        }
//    }

//    public async Task<BaseDocument> GetDocumentAsync(string documentId)
//    {
//        return await _context.Documents
//            .FirstOrDefaultAsync(d => d.DocumentId == documentId);
//    }

//    public async Task<BaseDocument> GetDocumentByTypeAndIdAsync(DocumentType type, string documentId)
//    {
//        return await _context.Documents
//            .FirstOrDefaultAsync(d => d.Type == type && d.DocumentId == documentId);
//    }

//    public async Task UpdateDocumentStatusAsync(string documentId, ProcessingStatus status)
//    {
//        try
//        {
//            var document = await _context.Documents
//                .FirstOrDefaultAsync(d => d.DocumentId == documentId);

//            if (document != null)
//            {
//                document.Status = status;
//                document.ProcessedAt = DateTime.UtcNow;

//                if (status == ProcessingStatus.Failed && document.RetryCount < 3)
//                {
//                    document.RetryCount++;
//                }

//                await _context.SaveChangesAsync();
//                _logger.LogInformation($"Document {documentId} status updated to {status}");
//            }
//            else
//            {
//                _logger.LogWarning($"Document {documentId} not found for status update");
//            }
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, $"Error updating document {documentId} status");
//            throw;
//        }
//    }

//    // Batch operations
//    public async Task<List<BaseDocument>> GetPendingDocumentsAsync()
//    {
//        return await _context.Documents
//            .Where(d => d.Status == ProcessingStatus.Received
//                       || d.Status == ProcessingStatus.Queued)
//            .OrderBy(d => d.ReceivedAt)
//            .Take(100) // Limit batch size
//            .ToListAsync();
//    }

//    public async Task<List<BaseDocument>> GetDocumentsByDateRangeAsync(DateTime startDate, DateTime endDate)
//    {
//        return await _context.Documents
//            .Where(d => d.ReceivedAt >= startDate && d.ReceivedAt <= endDate)
//            .OrderByDescending(d => d.ReceivedAt)
//            .ToListAsync();
//    }

//    // Check operations
//    public async Task<bool> DocumentExistsAsync(string documentId)
//    {
//        return await _context.Documents
//            .AnyAsync(d => d.DocumentId == documentId);
//    }

//    public async Task<bool> OrderExistsAsync(string orderId)
//    {
//        return await _context.Orders
//            .AnyAsync(o => o.OrderId == orderId);
//    }
//}
//}