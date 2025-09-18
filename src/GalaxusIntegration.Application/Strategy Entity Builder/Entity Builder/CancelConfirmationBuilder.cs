using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Application.Interfaces;
using GalaxusIntegration.Core.Entities;

namespace GalaxusIntegration.Application.Strategy_Builder.Entity_Builder;

public class CancelConfirmationBuilder : IEntityBuilder
{
    public CancelConfirmationBuilder()
    {
    }

    public async Task<object> Build(UnifiedDocumentDto document)
    {
        if (document == null) throw new ArgumentNullException(nameof(document));

        var cancelConfirmation = new CancelConfirmation();

        // ===== Basic header info =====
        var header = document.Header;
        var info = header?.Metadata;
        var ctrl = header?.ControlInfo;

        cancelConfirmation.GenerationDate = ctrl?.GenerationDate;
        cancelConfirmation.OrderId = info?.OrderId ?? info?.DocumentId;
        cancelConfirmation.CancelConfirmationDate = info?.DocumentDate;
                                                    

        // ===== Items =====
        cancelConfirmation.Items = new List<CancelConfirmationItem>();
        if (document.ItemList?.Items != null)
        {
            foreach (var i in document.ItemList.Items)
            {
                if (i == null) continue;

                var item = new CancelConfirmationItem
                {
                    SupplierId = i.ProductDetails?.SupplierProductId?.Value ?? string.Empty,
                    InternationalId = i.ProductDetails?.InternationalProductId?.Value ?? string.Empty,
                    BuyerId = i.ProductDetails?.BuyerProductId?.Value ?? string.Empty,
                    Quantity = i.Quantity ?? 0m,
                    RequestAccepted = i.IsRequestAccepted ?? false,
                    ResponseComment = i.ResponseComment
                };

                cancelConfirmation.Items.Add(item);
            }
        }

        return cancelConfirmation;
    }
}