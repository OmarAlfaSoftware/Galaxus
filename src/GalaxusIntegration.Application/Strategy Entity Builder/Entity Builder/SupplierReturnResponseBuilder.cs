using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Application.Interfaces;
using GalaxusIntegration.Core.Entities;

namespace GalaxusIntegration.Application.Strategy_Builder.Entity_Builder;

public class SupplierReturnNotificationBuilder : IEntityBuilder
{
    public SupplierReturnNotificationBuilder()
    {
    }

    public async Task<object> Build(UnifiedDocumentDto document)
    {
        if (document == null) throw new ArgumentNullException(nameof(document));

        var supplierReturn = new SupplierReturnNotification();

        // ===== Basic header info =====
        var header = document.Header;
        var info = header?.Metadata;
        var ctrl = header?.ControlInfo;

        supplierReturn.GenerationDate = ctrl?.GenerationDate;
        supplierReturn.OrderId = info?.OrderId ?? info?.DocumentId;
        supplierReturn.SupplierReturnNotificationDate = info?.DocumentDate;

        // ===== Items =====
        supplierReturn.Items = new List<SupplierReturnItem>();
        if (document.ItemList?.Items != null)
        {
            foreach (var i in document.ItemList.Items)
            {
                if (i == null) continue;

                var item = new SupplierReturnItem
                {
                    SupplierId = i.ProductDetails?.SupplierProductId?.Value ?? string.Empty,
                    InternationalId = i.ProductDetails?.InternationalProductId?.Value ?? string.Empty,
                    BuyerId = i.ProductDetails?.BuyerProductId?.Value ?? string.Empty,
                    Quantity = i.Quantity ?? 0m,
                    RequestAccepted = i.IsRequestAccepted ?? false,
                    ResponseComment = i.ResponseComment
                };

                supplierReturn.Items.Add(item);
            }
        }

        return supplierReturn;
    }
}