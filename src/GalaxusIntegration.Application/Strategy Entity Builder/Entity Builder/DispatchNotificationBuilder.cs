// src/GalaxusIntegration.Application/Strategy Entity Builder/Entity Builder/DispatchNotificationBuilder.cs
using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Application.Interfaces;
using GalaxusIntegration.Core.Entities;

namespace GalaxusIntegration.Application.Strategy_Builder.Entity_Builder;

public class DispatchNotificationBuilder : IEntityBuilder
{
    public async Task<object> Build(UnifiedDocumentDTO document)
    {
        if (document == null) throw new ArgumentNullException(nameof(document));

        var dispatch = new DispatchNotification();

        var header = document.Header;
        var info = header?.Info;

        // Basic info
        dispatch.DispatchNotificationId = info?.DispatchNotificationId ?? GenerateDispatchId();
        dispatch.DispatchNotificationDate = info?.DocumentDate ?? DateTime.UtcNow;
        dispatch.GenerationDate = header?.ControlInfo?.GenerationDate ?? DateTime.UtcNow;

        // Delivery party
        if (info?.Parties?.PartyList != null)
        {
            var deliveryParty = info.Parties.PartyList
                .FirstOrDefault(p => p.PartyRole?.ToLower() == "delivery");

            if (deliveryParty != null)
            {
                dispatch.DeliveryAddress = MapDeliveryAddress(deliveryParty);
            }
        }

        // Shipment info
        dispatch.ShipmentId = info?.InfoId;
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
                    ProductId = item.ProductId?.SupplierPid?.Value,
                    InternationalId = item.ProductId?.InternationalPid?.Value,
                    BuyerId = item.ProductId?.BuyerPid?.Value,
                    Quantity = item.Quantity ?? 0m,
                    OrderId = item.OrderId,
                    SerialNumbers = item.ProductId?.SerialNumbers ?? new List<string>()
                };

                // Logistic details (SSCC)
                if (item.LogisticDetails != null)
                {
                    dispatchItem.LogisticDetails.Packages = item?.LogisticDetails.PackageInfo.Packages.Select(p => new Package() { PackageId = p.PackageId, PackageQuantity = p.Quantity }).ToList();
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

    private DeliveryAddress MapDeliveryAddress(DocumentParty party)
    {
        var addr = party.Address;
        return new DeliveryAddress
        {
            Name = addr?.Name,
            Name2 = addr?.Name2,
            Name3 = addr?.Name3,
            Department = addr?.Department,
            ContactFirstName = addr?.ContactDetails?.FirstName,
            ContactLastName = addr?.ContactDetails?.ContactName,
            Street = addr?.Street,
            Zip = addr?.Zip,
            BoxNo = addr?.BoxNo,
            City = addr?.City,
            Country = addr?.Country,
            CountryCode = addr?.CountryCoded
        };
    }
}