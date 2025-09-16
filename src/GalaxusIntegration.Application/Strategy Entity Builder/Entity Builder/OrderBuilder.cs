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
    public async Task<object> Build(UnifiedDocumentDTO document) 
    {
        if (document == null) throw new ArgumentNullException(nameof(document));

        var order = new Order();

        // ===== Basic header info =====
        var header = document.Header;
        var info = header?.Info;
        var ctrl = header?.ControlInfo;
        var summary = document.Summary;

        order.GenerationDate = ctrl?.GenerationDate;
        order.OrderId = info?.OrderId ?? info?.DocumentId;
        order.CustOrderId = info?.CustomerOrderReference?.OrderId;
        order.OrderDate = info?.OrderDate ?? info?.DocumentDate ?? DateTime.MinValue;
        order.Language = info?.Language;
        order.BuyerId = info?.OrderPartiesReference?.BuyerIdRef;
        order.SupplierId = info?.OrderPartiesReference?.SupplierIdRef;
        order.TotalAmount = summary?.TotalAmount ?? 0m;
        order.Currency = info?.Currency;

        // ===== HeaderUDX =====
        if (info?.HeaderUDX != null)
        {
            order.HeaderUDX = new Core.Entities.HeaderUDX
            {
                CustomerType = info.HeaderUDX.CustomerType,
                DeliveryType = info.HeaderUDX.DeliveryType,
                IsSaturdayAllowed = info.HeaderUDX.SaturdayDeliveryAllowed,
                IsCollectiveOrder = info.HeaderUDX.IsCollectiveOrder,
                CustomerOrderRef = info.HeaderUDX.EndCustomerOrderReference,
                PhysicalDeliveryNote = info.HeaderUDX.PhysicalDeliveryNoteRequired 
            };
        }

        // ===== Parties =====
        order.Parties = new List<Core.Entities.Party>();
        if (info?.Parties?.PartyList != null)
        {
            foreach (var p in info.Parties.PartyList)
            {
                if (p == null) continue;

                var addr = p.Address;
                var party = new Core.Entities.Party
                {
                    PartyData = new PartyData
                    {
                        Name = addr?.Name,
                        Name2 = addr?.Name2,
                        Name3 = addr?.Name3,
                        Department = addr?.Department,
                        Title = addr?.ContactDetails?.Title,
                        FirstName = addr?.ContactDetails?.FirstName,
                        ContactName = addr?.ContactDetails?.ContactName,
                        Street = addr?.Street,
                        Zip = addr?.Zip,
                        BoxNo = addr?.BoxNo,
                        City = addr?.City,
                        CountryCode = addr?.CountryCoded,
                        Country = addr?.Country,
                        Email = addr?.Email,
                        Phone = addr?.Phone,
                        VatId = addr?.VatId
                    }
                };

                // NOTE: PartyHeaders لازم تكون public في الـ Domain
                // لو عندك setter:
                // party.PartyHeaders = new List<PartyHeader>
                // {
                //     new PartyHeader
                //     {
                //         PartyId   = p.PartyIds?.FirstOrDefault()?.Value,
                //         PartyRole = p.PartyRole
                //     }
                // };

                order.Parties.Add(party);
            }
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
                    SupplierId = i.ProductId?.SupplierPid?.Value ?? string.Empty,
                    InternationalId = i.ProductId?.InternationalPid?.Value ?? string.Empty,
                    BuyerId = i.ProductId?.BuyerPid?.Value ?? string.Empty,
                    Description = i.ProductId?.DescriptionShort,
                    Quantity = i.Quantity ?? 0m,
                    Unit = i.OrderUnit,
                    UnitPrice = i.ProductPriceFix != null ? (decimal)i.ProductPriceFix.Amount : 0m,
                    TotalPrice = i.PriceLineAmount.HasValue
                                        ? (decimal)i.PriceLineAmount.Value
                                        : (i.Quantity ?? 0m) * (i.ProductPriceFix != null ? (decimal)i.ProductPriceFix.Amount : 0m)
                };

                order.Items.Add(item);
            }
        }

        // ===== DeliveryInfo aggregate =====
        var starts = document.ItemList?.Items?
                         .Select(x => x?.DeliveryDate?.StartDate)
                         .Where(d => d.HasValue)
                         .Select(d => d!.Value)
                         .ToList() ?? new List<DateTime>();
        var ends = document.ItemList?.Items?
                         .Select(x => x?.DeliveryDate?.EndDate)
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