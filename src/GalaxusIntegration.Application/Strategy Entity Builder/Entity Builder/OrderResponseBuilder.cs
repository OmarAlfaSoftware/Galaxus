// src/GalaxusIntegration.Application/Strategy Entity Builder/Entity Builder/OrderResponseBuilder.cs
using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Application.Interfaces;
using GalaxusIntegration.Core.Entities;

namespace GalaxusIntegration.Application.Strategy_Builder.Entity_Builder;

public class OrderResponseBuilder : IEntityBuilder
{
    public async Task<object> Build(UnifiedDocumentDto document)
    {
        if (document == null) throw new ArgumentNullException(nameof(document));

        var orderResponse = new OrderResponse();

        var header = document.Header;
        var info = header?.Metadata;

        // Basic info
        orderResponse.OrderId = info?.OrderId;
        orderResponse.OrderResponseDate = info?.DocumentDate ?? DateTime.UtcNow;
        orderResponse.SupplierOrderId = GenerateSupplierOrderId();

        // Response items
        orderResponse.ResponseItems = new List<Core.Entities.OrderResponseItem>();
        if (document.ItemList?.Items != null)
        {
            foreach (var item in document.ItemList.Items)
            {
                if (item == null) continue;

                var responseItem = new Core.Entities.OrderResponseItem
                {
                    ProductId = item.ProductDetails?.SupplierProductId?.Value,
                    InternationalId = item.ProductDetails?.InternationalProductId?.Value,
                    BuyerId = item.ProductDetails?.BuyerProductId?.Value,
                    Quantity = item.Quantity ?? 0m,
                    DeliveryStartDate = item.ItemDeliveryDateRange?.EarliestDate,
                    DeliveryEndDate = item.ItemDeliveryDateRange?.LatestDate
                };

                orderResponse.ResponseItems.Add(responseItem);
            }
        }

        return orderResponse;
    }

    private string GenerateSupplierOrderId()
    {
        return $"SUP-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
    }
}