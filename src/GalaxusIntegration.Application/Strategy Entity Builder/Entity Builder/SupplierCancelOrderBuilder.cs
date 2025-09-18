using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Application.Interfaces;
using GalaxusIntegration.Core.Entities;

namespace GalaxusIntegration.Application.Strategy_Builder.Entity_Builder;

public class SupplierCancelNotificationBuilder : IEntityBuilder
{
    public SupplierCancelNotificationBuilder()
    {
    }

    public async Task<object> Build(UnifiedDocumentDto document)
    {
        if (document == null) throw new ArgumentNullException(nameof(document));

        var supplierCancel = new SupplierCancelNotification();

        // ===== Basic header info =====
        var header = document.Header;
        var info = header?.Metadata;
        var ctrl = header?.ControlInfo;

        supplierCancel.GenerationDate = ctrl?.GenerationDate;
        supplierCancel.OrderId = info?.OrderId ?? info?.DocumentId;
        supplierCancel.SupplierCancelNotificationDate = info?.DocumentDate;

        // ===== Items =====
        supplierCancel.Items = new List<SupplierCancelItem>();
        if (document.ItemList?.Items != null)
        {
            foreach (var i in document.ItemList.Items)
            {
                if (i == null) continue;

                var item = new SupplierCancelItem
                {
                    SupplierId = i.ProductDetails?.SupplierProductId?.Value ?? string.Empty,
                    InternationalId = i.ProductDetails?.InternationalProductId?.Value ?? string.Empty,
                    BuyerId = i.ProductDetails?.BuyerProductId?.Value ?? string.Empty,
                    Quantity = i.Quantity ?? 0m
                };

                supplierCancel.Items.Add(item);
            }
        }

        return supplierCancel;
    }
}