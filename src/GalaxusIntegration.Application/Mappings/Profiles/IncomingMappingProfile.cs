using AutoMapper;
using GalaxusIntegration.Application.DTOs.Incoming;
using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Application.DTOs.PartialDTOs;
using GalaxusIntegration.Core.Entities;
using GalaxusIntegration.Shared.Enum;
using System.Collections.Generic;
using ControlInfo = GalaxusIntegration.Application.DTOs.Internal.ControlInfo;
using Party = GalaxusIntegration.Core.Entities.Party;

namespace GalaxusIntegration.Application.Mappings.Profiles;

public class IncomingMappingProfile : Profile
{
    public IncomingMappingProfile()
    {
        // =================== Root: Order ===================
        CreateMap<UnifiedDocumentDTO, Order>()
            .ForMember(d => d.GenerationDate, opt => opt.MapFrom(s =>
                s.Header != null && s.Header.ControlInfo != null
                    ? s.Header.ControlInfo.GenerationDate
                    : (DateTime?)null))
            .ForMember(d => d.OrderId, opt => opt.MapFrom(s =>
                s.Header != null && s.Header.Info != null
                    ? (s.Header.Info.OrderId ?? s.Header.Info.DocumentId)
                    : null))
            .ForMember(d => d.CustOrderId, opt => opt.MapFrom(s =>
                s.Header != null && s.Header.Info != null && s.Header.Info.CustomerOrderReference != null
                    ? s.Header.Info.CustomerOrderReference.OrderId
                    : null))
            .ForMember(d => d.OrderDate, opt => opt.MapFrom(s =>
                s.Header != null && s.Header.Info != null
                    ? (s.Header.Info.OrderDate ?? s.Header.Info.DocumentDate ?? DateTime.MinValue)
                    : DateTime.MinValue))
            .ForMember(d => d.Language, opt => opt.MapFrom(s =>
                s.Header != null && s.Header.Info != null ? s.Header.Info.Language : null))
            .ForMember(d => d.BuyerId, opt => opt.MapFrom(s =>
                s.Header != null && s.Header.Info != null && s.Header.Info.OrderPartiesReference != null
                    ? s.Header.Info.OrderPartiesReference.BuyerIdRef
                    : null))
            .ForMember(d => d.SupplierId, opt => opt.MapFrom(s =>
                s.Header != null && s.Header.Info != null && s.Header.Info.OrderPartiesReference != null
                    ? s.Header.Info.OrderPartiesReference.SupplierIdRef
                    : null))
            .ForMember(d => d.TotalAmount, opt => opt.MapFrom(s =>
                s.Summary != null && s.Summary.TotalAmount.HasValue
                    ? (decimal)s.Summary.TotalAmount.Value
                    : 0m))
            .ForMember(d => d.Currency, opt => opt.MapFrom(s =>
                s.Header != null && s.Header.Info != null ? s.Header.Info.Currency : null))
            .ForMember(d => d.HeaderUDX, opt => opt.MapFrom(s =>
                s.Header != null && s.Header.Info != null ? s.Header.Info.HeaderUDX : null))
            .ForMember(d => d.Parties, opt => opt.MapFrom(s =>
                s.Header != null && s.Header.Info != null && s.Header.Info.Parties != null
                    ? s.Header.Info.Parties
                    : new()))
            .ForMember(d => d.Items, opt => opt.MapFrom(s =>
                s.ItemList != null && s.ItemList.Items != null
                    ? s.ItemList.Items
                    : new List<DocumentItem>()))
            .ForMember(d => d.DeliveryInfo, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                // AfterMap بيستخدم delegate عادي (مش expression tree) فممكن نستعمل ?. هنا بأمان
                var starts = src.ItemList?.Items?
                                .Select(i => i?.DeliveryDate?.StartDate)
                                .Where(dt => dt.HasValue)
                                .Select(dt => dt!.Value)
                                .ToList() ?? new List<DateTime>();

                var ends = src.ItemList?.Items?
                                .Select(i => i?.DeliveryDate?.EndDate)
                                .Where(dt => dt.HasValue)
                                .Select(dt => dt!.Value)
                                .ToList() ?? new List<DateTime>();

                if (starts.Any() || ends.Any())
                {
                    dest.DeliveryInfo ??= new DeliveryInfo();
                    if (starts.Any()) dest.DeliveryInfo.StartDate = starts.Min();
                    if (ends.Any()) dest.DeliveryInfo.EndDate = ends.Max();
                }
            });

        // =================== HeaderUDX ===================
        CreateMap<GalaxusIntegration.Application.DTOs.Internal.HeaderUDX, Core.Entities.HeaderUDX>()
            .ForMember(d => d.CustomerType, opt => opt.MapFrom(s => s.CustomerType))
            .ForMember(d => d.DeliveryType, opt => opt.MapFrom(s => s.DeliveryType))
            .ForMember(d => d.IsSaturdayAllowed, opt => opt.MapFrom(s => s.SaturdayDeliveryAllowed))
            .ForMember(d => d.IsCollectiveOrder, opt => opt.MapFrom(s => s.IsCollectiveOrder))
            .ForMember(d => d.CustomerOrderRef, opt => opt.MapFrom(s => s.EndCustomerOrderReference))
            .ForMember(d => d.PhysicalDeliveryNote, opt => opt.MapFrom(s => s.PhysicalDeliveryNoteRequired ? "Required" : "NotRequired"));

        // =================== Parties (list) ===================
        CreateMap<DTOs.PartialDTOs.Parties, List<Core.Entities.Party>>()
            .ConvertUsing(src => MapPartiesSafe(src));

        // =================== Items ===================
        CreateMap<DocumentItem, Core.Entities.OrderItem>()
            .ForMember(d => d.LineItemId, opt => opt.MapFrom(s =>
                s.LineItemId ?? string.Empty))
            .ForMember(d => d.SupplierId, opt => opt.MapFrom(s =>
                s.ProductId != null && s.ProductId.SupplierPid != null
                    ? s.ProductId.SupplierPid.Value
                    : string.Empty))
            .ForMember(d => d.InternationalId, opt => opt.MapFrom(s =>
                s.ProductId != null && s.ProductId.InternationalPid != null
                    ? s.ProductId.InternationalPid.Value
                    : string.Empty))
            .ForMember(d => d.BuyerId, opt => opt.MapFrom(s =>
                s.ProductId != null && s.ProductId.BuyerPid != null
                    ? s.ProductId.BuyerPid.Value
                    : string.Empty))
            .ForMember(d => d.Description, opt => opt.MapFrom(s =>
                s.ProductId != null ? s.ProductId.DescriptionShort : null))
            .ForMember(d => d.Quantity, opt => opt.MapFrom(s =>
                s.Quantity.HasValue ? s.Quantity.Value : 0m))
            .ForMember(d => d.Unit, opt => opt.MapFrom(s =>
                s.OrderUnit))
            .ForMember(d => d.UnitPrice, opt => opt.MapFrom(s =>
                s.ProductPriceFix != null ? (decimal)s.ProductPriceFix.Amount : 0m))
            .ForMember(d => d.TotalPrice, opt => opt.MapFrom(s =>
                s.PriceLineAmount.HasValue
                    ? (decimal)s.PriceLineAmount.Value
                    : (s.Quantity.HasValue ? s.Quantity.Value : 0m) *
                      (s.ProductPriceFix != null ? (decimal)s.ProductPriceFix.Amount : 0m)
            ));
    }

    // ============ Helpers ============
    private static List<Core.Entities.Party> MapPartiesSafe(DTOs.PartialDTOs.Parties src)
    {
        var list = new List<Core.Entities.Party>();
        if (src == null || src.PartyList == null) return list;

        foreach (var p in src.PartyList)
        {
            var addr = p != null ? p.Address : null;

            var party = new Core.Entities.Party
            {
                PartyData = new Core.Entities.PartyData
                {
                    Name = addr != null ? addr.Name : null,
                    Name2 = addr != null ? addr.Name2 : null,
                    Name3 = addr != null ? addr.Name3 : null,
                    Department = addr != null ? addr.Department : null,
                    Title = (addr != null && addr.ContactDetails != null) ? addr.ContactDetails.Title : null,
                    FirstName = (addr != null && addr.ContactDetails != null) ? addr.ContactDetails.FirstName : null,
                    ContactName = (addr != null && addr.ContactDetails != null) ? addr.ContactDetails.ContactName : null,
                    Street = addr != null ? addr.Street : null,
                    Zip = addr != null ? addr.Zip : null,
                    BoxNo = addr != null ? addr.BoxNo : null,
                    City = addr != null ? addr.City : null,
                    CountryCode = addr != null ? addr.CountryCoded : null,
                    Country = addr != null ? addr.Country : null,
                    Email = addr != null ? addr.Email : null,
                    Phone = addr != null ? addr.Phone : null,
                    VatId = addr != null ? addr.VatId : null
                }
            };

            // ملاحظة: لو PartyHeaders لازم تتعبّى هنا، خليه public أو وفّر method في الـ Domain.
            // Example if public:
            // party.PartyHeaders = new List<PartyHeader>
            // {
            //     new PartyHeader
            //     {
            //         PartyRole = p != null ? p.PartyRole : null,
            //         PartyId   = p != null && p.PartyIds != null && p.PartyIds.Count > 0 ? p.PartyIds[0].Value : null
            //     }
            // };

            list.Add(party);
        }

        return list;
    }
}
// Placeholder DTOs - you'll need to create these
public class UnifiedToOrderProfile : Profile
{
    public UnifiedToOrderProfile()
    {
        // ===== Root =====
        CreateMap<UnifiedDocumentDTO, OrderDTO>()
            .ForMember(d => d.Version, opt => opt.MapFrom(s => s.Version))
            .ForMember(d => d.Type, opt => opt.MapFrom(s => s.Type))
            .ForMember(d => d.OrderHeader, opt => opt.MapFrom(s => s.Header))
            .ForMember(d => d.OrderItemList, opt => opt.MapFrom(s => s.ItemList))
            .ForMember(d => d.OrderSummary, opt => opt.MapFrom(s => s.Summary));

        // ===== Header =====
        CreateMap<DocumentHeader, Header>()
            .ForMember(d => d.ControlInfo, opt => opt.MapFrom(s => s.ControlInfo))
            .ForMember(d => d.OrderInfo, opt => opt.MapFrom(s => s.Info));

        // ControlInfo: target is non-nullable DateTime → pick a safe fallback
        CreateMap<ControlInfo,GalaxusIntegration.Application.DTOs.PartialDTOs.ControlInfo>()
            .ForMember(d => d.GenerationDate, opt => opt.MapFrom(s => s.GenerationDate ?? DateTime.UtcNow));

        // ===== OrderInfo =====
        CreateMap<DocumentInfo, OrderInfo>()
            .ForMember(d => d.OrderId, opt => opt.MapFrom(s => s.OrderId ?? s.DocumentId))
            .ForMember(d => d.OrderDate, opt => opt.MapFrom(s => s.OrderDate ?? s.DocumentDate ?? DateTime.MinValue))
            .ForMember(d => d.Language, opt => opt.MapFrom(s => s.Language))
            .ForMember(d => d.Currency, opt => opt.MapFrom(s => s.Currency))
            .ForMember(d => d.Parties, opt => opt.MapFrom(s => s.Parties))
            .ForMember(d => d.CustomerOrderReference, opt => opt.MapFrom(s => s.CustomerOrderReference))
            .ForMember(d => d.OrderPartiesReference, opt => opt.MapFrom(s => s.OrderPartiesReference))
            .ForMember(d => d.HeaderUDX, opt => opt.MapFrom(s => s.HeaderUDX));

        // Parties
        CreateMap<GalaxusIntegration.Application.DTOs.Internal.Parties, GalaxusIntegration.Application.DTOs.PartialDTOs.Parties>()
            .ForMember(d => d.PartyList, opt => opt.MapFrom(s => s.PartyList));

        CreateMap<GalaxusIntegration.Application.DTOs.Internal.DocumentParty, GalaxusIntegration.Application.DTOs.PartialDTOs.Party>()
            .ForMember(d => d.PartyRole, opt => opt.MapFrom(s => s.PartyRole))
            .ForMember(d => d.PartyIds, opt => opt.MapFrom(s => s.PartyIds))
            .ForMember(d => d.Address, opt => opt.MapFrom(s => s.Address));

        CreateMap<GalaxusIntegration.Application.DTOs.Internal.PartyId, GalaxusIntegration.Application.DTOs.PartialDTOs.PartyId>()
            .ForMember(d => d.Type, opt => opt.MapFrom(s => s.Type))
            .ForMember(d => d.Value, opt => opt.MapFrom(s => s.Value));

        // Address: map what we have; leave extras null
        CreateMap<GalaxusIntegration.Application.DTOs.Internal.Address, GalaxusIntegration.Application.DTOs.PartialDTOs.Address>()
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
            .ForMember(d => d.Name2, opt => opt.Ignore())
            .ForMember(d => d.Name3, opt => opt.Ignore())
            .ForMember(d => d.Department, opt => opt.Ignore())
            .ForMember(d => d.Street, opt => opt.MapFrom(s => s.Street))
            .ForMember(d => d.Zip, opt => opt.MapFrom(s => s.Zip))
            .ForMember(d => d.BoxNo, opt => opt.Ignore())
            .ForMember(d => d.City, opt => opt.MapFrom(s => s.City))
            .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Country))
            .ForMember(d => d.CountryCoded, opt => opt.Ignore())
            .ForMember(d => d.Phone, opt => opt.MapFrom(s => s.Phone))
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.VatId, opt => opt.Ignore())
            .ForMember(d => d.ContactDetails, opt => opt.Ignore());

        // CustomerOrderReference: Unified has string? OrderId → target expects int?
        CreateMap<GalaxusIntegration.Application.DTOs.Internal.CustomerOrderRefernce, GalaxusIntegration.Application.DTOs.PartialDTOs.CustomerOrderRefernce>()
            .ForMember(d => d.OrderId, opt => opt.MapFrom(s =>s.OrderId));

        // OrderPartiesReference: Unified has string ids only (no "type")
        CreateMap<GalaxusIntegration.Application.DTOs.Internal.OrderPartiesReference, GalaxusIntegration.Application.DTOs.PartialDTOs.OrderPartiesReference>()
            .ForMember(d => d.BuyerIdRef, opt => opt.MapFrom(s => new BuyerIdRef { Type = null, Value = s.BuyerIdRef }))
            .ForMember(d => d.SupplierIdRef, opt => opt.MapFrom(s => new SupplierIdRef { Type = null, Value = s.SupplierIdRef }));

        // HeaderUDX
        CreateMap<GalaxusIntegration.Application.DTOs.Internal.HeaderUDX, GalaxusIntegration.Application.DTOs.PartialDTOs.HeaderUDX>()
            .ForMember(d => d.CustomerType, opt => opt.MapFrom(s => s.CustomerType))
            .ForMember(d => d.DeliveryType, opt => opt.MapFrom(s => s.DeliveryType))
            .ForMember(d => d.SaturdayDeliveryAllowed, opt => opt.MapFrom(s => s.SaturdayDeliveryAllowed))
            .ForMember(d => d.IsCollectiveOrder, opt => opt.MapFrom(s => s.IsCollectiveOrder))
            .ForMember(d => d.EndCustomerOrderReference, opt => opt.MapFrom(s => s.EndCustomerOrderReference))
            .ForMember(d => d.PhysicalDeliveryNoteRequired, opt => opt.MapFrom(s => s.PhysicalDeliveryNoteRequired));

        // ===== Items =====
        CreateMap<DocumentItemList, ItemList>()
            .ForMember(d => d.Items, opt => opt.MapFrom(s => s.Items));

        CreateMap<DocumentItem, GalaxusIntegration.Application.DTOs.PartialDTOs.OrderItem>()
            .ForMember(d => d.LineItemId, opt => opt.MapFrom(s => s.LineItemId))
            .ForMember(d => d.ProductId, opt => opt.MapFrom(s => s.ProductId))
            .ForMember(d => d.Quantity, opt => opt.MapFrom(s => s.Quantity.HasValue ? (double)s.Quantity.Value : 0d))
            .ForMember(d => d.OrderUnit, opt => opt.MapFrom(s => s.OrderUnit))
            .ForMember(d => d.ProductPriceFix, opt => opt.MapFrom(s => s.ProductPriceFix))
            .ForMember(d => d.PriceLineAmount, opt => opt.MapFrom(s => s.PriceLineAmount.HasValue ? (double)s.PriceLineAmount.Value : 0d))
            .ForMember(d => d.DeliveryDate, opt => opt.MapFrom(s => s.DeliveryDate));

        // ProductDetails (Unified → XML ProductDetails with 3 PIDs)
        CreateMap<GalaxusIntegration.Application.DTOs.Internal.ProductDetails, GalaxusIntegration.Application.DTOs.PartialDTOs.ProductDetails>()
            .ForMember(d => d.SupplierPid, opt => opt.MapFrom(s => s.SupplierPid))
            .ForMember(d => d.BuyerPid, opt => opt.MapFrom(s => s.BuyerPid))
            .ForMember(d => d.InternationalPid, opt => opt.MapFrom(s => s.InternationalPid))
            .ForMember(d => d.DescriptionShort, opt => opt.MapFrom(s => s.DescriptionShort));

        CreateMap<PidWithType, SupplierPid>()
            .ForMember(d => d.Type, opt => opt.MapFrom(s => s.Type))
            .ForMember(d => d.Value, opt => opt.MapFrom(s => s.Value));
        CreateMap<PidWithType, BuyerPid>()
            .ForMember(d => d.Type, opt => opt.MapFrom(s => s.Type))
            .ForMember(d => d.Value, opt => opt.MapFrom(s => s.Value));
        CreateMap<PidWithType, InternationalPid>()
            .ForMember(d => d.Type, opt => opt.MapFrom(s => s.Type))
            .ForMember(d => d.Value, opt => opt.MapFrom(s => s.Value));

        // Price (Unified has Amount/Currency; XML has PriceAmount + TaxDetailsFix but no currency on this node)
        CreateMap<GalaxusIntegration.Application.DTOs.Internal.ProductPriceFix, GalaxusIntegration.Application.DTOs.PartialDTOs.ProductPriceFix>()
            .ForMember(d => d.PriceAmount, opt => opt.MapFrom(s => (double)s.Amount))
            .ForMember(d => d.TaxDetailsFix, opt => opt.Ignore()); // no tax in Unified → leave null

        // DeliveryDate (Unified has Start/End only; XML also has "type" attribute)
        CreateMap<GalaxusIntegration.Application.DTOs.Internal.DeliveryDate, GalaxusIntegration.Application.DTOs.PartialDTOs.DeliveryDate>()
            .ForMember(d => d.Type, opt => opt.Ignore()) // unknown in Unified
            .ForMember(d => d.DeliveryStartDate, opt => opt.MapFrom(s => s.StartDate))
            .ForMember(d => d.DeliveryEndDate, opt => opt.MapFrom(s => s.EndDate));

        // ===== Summary =====
        CreateMap<DocumentSummary,DTOs.PartialDTOs.Summary>()
            .ForMember(d => d.TotalItemNum, opt => opt.MapFrom(s => s.TotalItemNum))
            .ForMember(d => d.TotalAmount, opt => opt.MapFrom(s => s.TotalAmount.HasValue ? (double)s.TotalAmount.Value : 0d));
    }
   }

public class ReturnItemDTO { }
