using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Application.Interfaces;
using GalaxusIntegration.Core.Entities;
namespace GalaxusIntegration.Application.Strategy_Builder.Entity_Builder;

using RecievedOrder=GalaxusIntegration.Core.Entities.Order;
public class OrderBuilder : IEntityBuilder
{
    public OrderBuilder()
    {
    }
    public async Task<object> Build(UnifiedDocumentDto document) 
    {
        if (document == null) throw new ArgumentNullException(nameof(document));

        var order = new Order();

        // ===== Basic header info =====
        var header = document.Header;
        var info = header?.Metadata;
        var ctrl = header?.ControlInfo;
        var summary = document.Summary;

        order.GenerationDate = ctrl?.GenerationDate;
        order.OrderId = info?.OrderId ?? info?.DocumentId;
        order.CustOrderId = info?.CustomerOrderReference?.OrderId;
        order.OrderDate = info?.OrderDate ?? info?.DocumentDate ?? DateTime.MinValue;
        order.Language = info?.Language;
        order.BuyerId = info?.OrderPartyReferences?.BuyerReferenceId;
        order.SupplierId = info?.OrderPartyReferences?.SupplierReferenceId;
        order.TotalAmount = summary?.TotalGrossAmount ?? 0m;
        order.Currency = info?.Currency;

        // ===== HeaderUDX =====
        if (info?.UserDefinedExtensions != null)
        {
            order.HeaderUDX = new Core.Entities.HeaderUDX
            {
                CustomerType = info?.UserDefinedExtensions.CustomerType,
                DeliveryType = info?.UserDefinedExtensions.DeliveryType,
                IsSaturdayAllowed = info?.UserDefinedExtensions.IsSaturdayDeliveryAllowed,
                IsCollectiveOrder = info?.UserDefinedExtensions.IsCollectiveOrder,
                CustomerOrderRef = info?.UserDefinedExtensions.EndCustomerOrderReference,
                PhysicalDeliveryNote = info?.UserDefinedExtensions.IsPhysicalDeliveryNoteRequired 
            };
        }

        // ===== Parties =====
        order.Parties = new List<Core.Entities.Party>();
        foreach (DTOs.Internal.Parties party in info?.Parties)
        {
            var invoiceParty = new Core.Entities.Party();
            invoiceParty.PartyRole = party.Role;
            invoiceParty.PartyHeaders = party.PartyList.Select(z => new PartyHeader() { PartyValue = z.PartyIdValue, PartyType = z.PartyIdType }).ToList();
            var address = party.Address;
            invoiceParty.PartyData = new()
            {
                Name = address.Name,
                Name2 = address.NameLine2,
                Name3 = address.NameLine3,
                BoxNo = address.PoBoxNumber,
                City = address.City,
                ContactName = address.Contact.LastName,
                Country = address.Country,
                CountryCode = address.CountryCode,
                Department = address.Department,
                Email = address.EmailAddress,
                FirstName = address.Contact.FirstName,
                Phone = address.PhoneNumber,
                Street = address.Street,
                Title = address.Contact.Title,
                VatId = address.VatIdentificationNumber,
                Zip = address.PostalCode,
            };

        }
        // ===== Items =====
        order.Items = new List<OrderItem>();
        if (document.ItemList?.Items != null)
        {
            foreach (var i in document.ItemList.Items)
            {
                if (i == null) continue;

                var item = new OrderItem
                {
                    LineItemId = i.LineItemId ?? string.Empty,
                    SupplierId = i.ProductDetails?.SupplierProductId?.Value ?? string.Empty,
                    InternationalId = i.ProductDetails?.InternationalProductId?.Value ?? string.Empty,
                    BuyerId = i.ProductDetails?.BuyerProductId?.Value ?? string.Empty,
                    Description = i.ProductDetails?.ShortDescription,
                    Quantity = i.Quantity ?? 0m,
                    Unit = i.OrderUnit,
                    UnitPrice = i.LineItemPrice != null ? (decimal)i.LineItemPrice.Amount : 0m,
                    TotalPrice = i?.LineTotalAmount ?? 0
                };

                order.Items.Add(item);
            }
        }

        // ===== DeliveryInfo aggregate =====
        var starts = document.ItemList?.Items?
                         .Select(x => x?.ItemDeliveryDateRange?.EarliestDate)
                         .Where(d => d.HasValue)
                         .Select(d => d!.Value)
                         .ToList() ?? new List<DateTime>();
        var ends = document.ItemList?.Items?
                         .Select(x => x?.ItemDeliveryDateRange?.LatestDate)
                         .Where(d => d.HasValue)
                         .Select(d => d!.Value)
                         .ToList() ?? new List<DateTime>();

        if (starts.Any() || ends.Any())
        {
            order.DeliveryInfo = new DeliveryInfo
            {
                StartDate = starts.Any() ? starts.Min() : DateTime.MinValue,
                EndDate = ends.Any() ? ends.Max() : DateTime.MinValue
            };
        }

        return  order;
    }


  
}