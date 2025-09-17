// src/GalaxusIntegration.Application/Strategy Entity Builder/Entity Builder/DispatchNotificationBuilder.cs
using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Application.Interfaces;
using GalaxusIntegration.Core.Entities;

namespace GalaxusIntegration.Application.Strategy_Builder.Entity_Builder;

public class DispatchNotificationBuilder : IEntityBuilder
{
    public async Task<object> Build(UnifiedDocumentDto document)
    {
        if (document == null) throw new ArgumentNullException(nameof(document));

        var dispatch = new DispatchNotification();

        var header = document.Header;
        var info = header?.Metadata;

        // Basic info
        dispatch.DispatchNotificationId = info?.DispatchNotificationId ?? GenerateDispatchId();
        dispatch.DispatchNotificationDate = info?.DocumentDate ?? DateTime.UtcNow;
        dispatch.GenerationDate = header?.ControlInfo?.GenerationDate ?? DateTime.UtcNow;

        // Delivery party
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

        // Shipment info
        dispatch.ShipmentId = info?.DeliveryNoteId;
        dispatch.ShipmentCarrier = MapShipmentCarrier(info?.ShipmentCarrier);

        // Dispatch items
        dispatch.DispatchItems = new List<DispatchItem>();
        if (document.ItemList?.Items != null)
        {
            foreach (var item in document.ItemList.Items)
            {
                if (item == null) continue;

                var dispatchItem = new DispatchItem
                {
                    ProductId = item.ProductDetails?.SupplierProductId?.Value,
                    InternationalId = item.ProductDetails?.InternationalProductId?.Value,
                    BuyerId = item.ProductDetails?.BuyerProductId?.Value,
                    Quantity = item.Quantity ?? 0m,
                    OrderId = item.ReferencedOrderId,
                    SerialNumbers = item.SerialNumbers ?? new List<string>()
                };

                // Logistic details (SSCC)
                if (item.LogisticsDetails != null)
                {
                    dispatchItem.LogisticDetails = new();
                    dispatchItem.LogisticDetails.Packages = new();
                    dispatchItem.LogisticDetails.Packages =  item?.LogisticsDetails.PackageInformation.Packages.Select(p => new Package() { PackageId = p.PackageId, PackageQuantity = p.PackageQuantity }).ToList();
                }

                dispatch.DispatchItems.Add(dispatchItem);
            }
        }

        return dispatch;
    }

    private string GenerateDispatchId()
    {
        return $"DISP-{DateTime.UtcNow:yyyyMMddHHmmss}";
    }

    private ShipmentCarrierEnum MapShipmentCarrier(string carrier)
    {
        return carrier?.ToLower() switch
        {
            "swisspost" => ShipmentCarrierEnum.SwissPost,
            "postlogistics" => ShipmentCarrierEnum.PostLogistics,
            "dhl" => ShipmentCarrierEnum.DHL,
            "dhlfreight" => ShipmentCarrierEnum.DHLFreight,
            "ups" => ShipmentCarrierEnum.UPS,
            "fedex" => ShipmentCarrierEnum.FedEx,
            _ => ShipmentCarrierEnum.Other
        };
    }

    private DeliveryAddress MapDeliveryAddress(Address party)
    {
        var addr = party;
        return new DeliveryAddress
        {
            Name = addr?.Name,
            Name2 = addr?.NameLine2,
            Name3 = addr?.NameLine3,
            Department = addr?.Department,
            ContactFirstName = addr?.Contact?.FirstName,
            ContactLastName = addr?.Contact?.LastName,
            Street = addr?.Street,
            Zip = addr?.PostalCode,
            BoxNo = addr?.PoBoxNumber,
            City = addr?.City,
            Country = addr?.Country,
            CountryCode = addr?.CountryCode
        };
    }
}