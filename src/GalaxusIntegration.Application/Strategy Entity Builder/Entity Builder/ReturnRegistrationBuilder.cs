// src/GalaxusIntegration.Application/Strategy Entity Builder/Entity Builder/ReturnRegistrationBuilder.cs
using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Application.Interfaces;
using GalaxusIntegration.Core.Entities;

namespace GalaxusIntegration.Application.Strategy_Builder.Entity_Builder;

public class ReturnRegistrationBuilder : IEntityBuilder
{
    public async Task<object> Build(UnifiedDocumentDto document)
    {
        if (document == null) throw new ArgumentNullException(nameof(document));

        var returnReg = new ReturnRegistration();

        var header = document.Header;
        var info = header?.Metadata;

        // Basic info
        returnReg.OrderId = info?.OrderId;
        returnReg.ReturnRegistrationId = info?.ReturnRegistrationId;
        returnReg.ReturnRegistrationDate = info?.ReturnRegistrationDate ?? DateTime.UtcNow;
        returnReg.Language = info?.Language;
        returnReg.ShipmentId = info?.ShipmentId;
        returnReg.TrackingTracingUrl = info?.TrackingUrl;
        returnReg.Parties = new();
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
            returnReg.Parties.Add(invoiceParty);
        }

        // Order parties reference
        if (info?.OrderPartyReferences != null)
        {
            returnReg.BuyerIdRef = info.OrderPartyReferences.BuyerReferenceId;
            returnReg.SupplierIdRef = info.OrderPartyReferences.SupplierReferenceId;
        }

        // Return items
        returnReg.ReturnItems = new List<ReturnItem>();
        if (document.ItemList?.Items != null)
        {
            foreach (var item in document.ItemList.Items)
            {
                if (item == null) continue;

                var returnItem = new ReturnItem
                {
                    LineItemId = item.LineItemId,
                    ProductId = item.ProductDetails?.SupplierProductId?.Value,
                    InternationalId = item.ProductDetails?.InternationalProductId?.Value,
                    BuyerId = item.ProductDetails?.BuyerProductId?.Value,
                    Quantity = item.Quantity ?? 0m,
                    OrderUnit = item.OrderUnit ?? "C62",
                    ReturnReason = MapReturnReason(item.ReturnReasonCode ?? 0)
                };

                returnReg.ReturnItems.Add(returnItem);
            }
        }

        returnReg.TotalItemNum = document.Summary?.TotalItemCount ?? 0;

        return returnReg;
    }

    private ReturnReasonEnum MapReturnReason(int reason)
    {
        return reason switch
        {
            1 => ReturnReasonEnum.DontLike,
            2 => ReturnReasonEnum.WrongSize,
            3 => ReturnReasonEnum.WrongProduct,
            4 => ReturnReasonEnum.DeliveryTooLate,
            5 => ReturnReasonEnum.DoesNotMatchDescription,
            6 => ReturnReasonEnum.WrongOrder,
            _ => ReturnReasonEnum.Other
        };
    }

   
}