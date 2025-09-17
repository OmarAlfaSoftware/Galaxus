// src/GalaxusIntegration.Application/Strategy Entity Builder/Entity Builder/CancelRequestBuilder.cs
using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Application.Interfaces;
using GalaxusIntegration.Core.Entities;

namespace GalaxusIntegration.Application.Strategy_Builder.Entity_Builder;

public class CancelRequestBuilder : IEntityBuilder
{
    public async Task<object> Build(UnifiedDocumentDto document)
    {
        if (document == null) throw new ArgumentNullException(nameof(document));

        var cancelRequest = new CancelRequest();

        var header = document.Header;
        var info = header?.Metadata;

        // Basic info
        cancelRequest.OrderId = info?.OrderId;
        cancelRequest.CancelRequestDate = info?.DocumentDate ?? DateTime.UtcNow;
        cancelRequest.Language = info?.Language;

		// Parties
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
		// Order parties reference
		if (info?.OrderPartyReferences != null)
        {
            cancelRequest.BuyerIdRef = info.OrderPartyReferences.BuyerReferenceId;
            cancelRequest.SupplierIdRef = info.OrderPartyReferences.SupplierReferenceId;
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
                    ProductId = item.ProductDetails?.SupplierProductId?.Value,
                    InternationalId = item.ProductDetails?.InternationalProductId?.Value,
                    BuyerId = item.ProductDetails?.BuyerProductId?.Value,
                    Quantity = item.Quantity ?? 0m,
                    OrderUnit = item.OrderUnit ?? "C62"
                };

                cancelRequest.ItemsToCancel.Add(cancelItem);
            }
        }

        cancelRequest.TotalItemNum = document.Summary?.TotalItemCount ?? 0;

        return cancelRequest;
    }

    private Party MapParty(Parties p)
    {
        var addr = p.Address;
        return new Party
        {
            PartyHeaders =p.PartyList.Select(z=>new PartyHeader() { PartyType=z.PartyIdType,PartyValue=z.PartyIdValue}).ToList(),
            PartyRole=p.Role,
            PartyData = new PartyData
            {
                Name = addr?.Name,
                Name2 = addr?.NameLine2,
                Name3 = addr?.NameLine3,
                Department = addr?.Department,
                Title = addr?.Contact?.Title,
                FirstName = addr?.Contact?.FirstName,
                ContactName = addr?.Contact?.LastName,
                Street = addr?.Street,
                Zip = addr?.PostalCode,
                BoxNo = addr?.PoBoxNumber,
                City = addr?.City,
                CountryCode = addr?.CountryCode,
                Country = addr?.Country,
                Email = addr?.EmailAddress,
                Phone = addr?.PhoneNumber
            }
        };
    }
}