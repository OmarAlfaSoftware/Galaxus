// src/GalaxusIntegration.Application/Strategy Entity Builder/Entity Builder/OrderResponseBuilder.cs
using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Application.DTOs.Outgoing;
using GalaxusIntegration.Application.Interfaces;
using GalaxusIntegration.Core.Entities;

namespace GalaxusIntegration.Application.Strategy_Builder.Entity_Builder;

public class OrderResponseBuilder : IEntityBuilder
{
    public async Task<object> Build(UnifiedDocumentDTO document)
    {
        if (document == null) throw new ArgumentNullException(nameof(document));

        var orderResponse = new OrderResponse();

        var header = document.Header;
        var info = header?.Info;

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
                    ProductId = item.ProductId?.SupplierPid?.Value,
                    InternationalId = item.ProductId?.InternationalPid?.Value,
                    BuyerId = item.ProductId?.BuyerPid?.Value,
                    Quantity = item.Quantity ?? 0m,
                    DeliveryStartDate = item.DeliveryDate?.StartDate,
                    DeliveryEndDate = item.DeliveryDate?.EndDate
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