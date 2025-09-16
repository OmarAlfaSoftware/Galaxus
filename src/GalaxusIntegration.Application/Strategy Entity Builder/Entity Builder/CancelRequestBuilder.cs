// src/GalaxusIntegration.Application/Strategy Entity Builder/Entity Builder/CancelRequestBuilder.cs
using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Application.Interfaces;
using GalaxusIntegration.Core.Entities;

namespace GalaxusIntegration.Application.Strategy_Builder.Entity_Builder;

public class CancelRequestBuilder : IEntityBuilder
{
    public async Task<object> Build(UnifiedDocumentDTO document)
    {
        if (document == null) throw new ArgumentNullException(nameof(document));

        var cancelRequest = new CancelRequest();

        var header = document.Header;
        var info = header?.Info;

        // Basic info
        cancelRequest.OrderId = info?.OrderId;
        cancelRequest.CancelRequestDate = info?.DocumentDate ?? DateTime.UtcNow;
        cancelRequest.Language = info?.Language;

        // Parties
        if (info?.Parties?.PartyList != null)
        {
            cancelRequest.Parties = new();
            foreach (var p in info.Parties.PartyList)
            {
                if (p == null) continue;

                var party = MapParty(p);
                cancelRequest.Parties.Add(party);
            }
        }

        // Order parties reference
        if (info?.OrderPartiesReference != null)
        {
            cancelRequest.BuyerIdRef = info.OrderPartiesReference.BuyerIdRef;
            cancelRequest.SupplierIdRef = info.OrderPartiesReference.SupplierIdRef;
        }

        // Items to cancel
        cancelRequest.ItemsToCancel = new List<CancelRequestItem>();
        if (document.ItemList?.Items != null)
        {
            foreach (var item in document.ItemList.Items)
            {
                if (item == null) continue;

                var cancelItem = new CancelRequestItem
                {
                    LineItemId = item.LineItemId,
                    ProductId = item.ProductId?.SupplierPid?.Value,
                    InternationalId = item.ProductId?.InternationalPid?.Value,
                    BuyerId = item.ProductId?.BuyerPid?.Value,
                    Quantity = item.Quantity ?? 0m,
                    OrderUnit = item.OrderUnit ?? "C62"
                };

                cancelRequest.ItemsToCancel.Add(cancelItem);
            }
        }

        cancelRequest.TotalItemNum = document.Summary?.TotalItemNum ?? 0;

        return cancelRequest;
    }

    private Party MapParty(DocumentParty p)
    {
        var addr = p.Address;
        return new Party
        {
            PartyHeaders =p.PartyIds.Select(z=>new PartyHeader() { PartyType=z.Type,PartyValue=z.Value}).ToList(),
            PartyRole=p.PartyRole,
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
                Phone = addr?.Phone
            }
        };
    }
}