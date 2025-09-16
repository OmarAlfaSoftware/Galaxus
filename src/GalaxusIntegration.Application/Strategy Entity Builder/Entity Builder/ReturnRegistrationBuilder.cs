// src/GalaxusIntegration.Application/Strategy Entity Builder/Entity Builder/ReturnRegistrationBuilder.cs
using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Application.Interfaces;
using GalaxusIntegration.Core.Entities;

namespace GalaxusIntegration.Application.Strategy_Builder.Entity_Builder;

public class ReturnRegistrationBuilder : IEntityBuilder
{
    public async Task<object> Build(UnifiedDocumentDTO document)
    {
        if (document == null) throw new ArgumentNullException(nameof(document));

        var returnReg = new ReturnRegistration();

        var header = document.Header;
        var info = header?.Info;

        // Basic info
        returnReg.OrderId = info?.OrderId;
        returnReg.ReturnRegistrationId = info?.ReturnRegistrationId;
        returnReg.ReturnRegistrationDate = info?.ReturnDate ?? DateTime.UtcNow;
        returnReg.Language = info?.Language;
        returnReg.ShipmentId = info?.InfoId;
        returnReg.TrackingTracingUrl = info?.TrackingUrl;

        // Parties
        if (info?.Parties?.PartyList != null)
        {
            returnReg.Parties = new List<Party>();
            foreach (var p in info.Parties.PartyList)
            {
                if (p == null) continue;

                var party = MapParty(p);
                returnReg.Parties.Add(party);
            }
        }

        // Order parties reference
        if (info?.OrderPartiesReference != null)
        {
            returnReg.BuyerIdRef = info.OrderPartiesReference.BuyerIdRef;
            returnReg.SupplierIdRef = info.OrderPartiesReference.SupplierIdRef;
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
                    ProductId = item.ProductId?.SupplierPid?.Value,
                    InternationalId = item.ProductId?.InternationalPid?.Value,
                    BuyerId = item.ProductId?.BuyerPid?.Value,
                    Quantity = item.Quantity ?? 0m,
                    OrderUnit = item.OrderUnit ?? "C62",
                    ReturnReason = MapReturnReason(item.ReturnReason ?? 0)
                };

                returnReg.ReturnItems.Add(returnItem);
            }
        }

        returnReg.TotalItemNum = document.Summary?.TotalItemNum ?? 0;

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

    private Party MapParty(DocumentParty p)
    {
        var addr = p.Address;
        return new Party
        {
            PartyRole = p.PartyRole,
            PartyHeaders = p.PartyIds.Select(z=>new PartyHeader() { PartyValue=z.Value,PartyType=z.Type}).ToList(),
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