using AutoMapper;
using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Core.Entities;
using GalaxusIntegration.Shared.Enum;

namespace GalaxusIntegration.Application.Mappings.Profiles;

public class IncomingMappingProfile : Profile
{
    public IncomingMappingProfile()
    {
        // Unified to specific incoming DTOs
        CreateMap<UnifiedDocumentDTO, Order>()
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Header.Info.OrderId))
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.Header.Info.OrderDate ?? DateTime.UtcNow))
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Header.Info.Currency))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.DocumentType))
            .ForMember(dest => dest.Direction, opt => opt.MapFrom(src => DocumentDirection.Incoming))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ProcessingStatus.Received))
            .ForMember(dest => dest.ReceivedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        
        // DTOs to Domain entities
        CreateMap<DocumentItem, OrderItem>()
            .ForMember(dest => dest.LineItemId, opt => opt.MapFrom(src => src.LineItemId))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId.SupplierPid.Value))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ProductId.DescriptionShort))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity ?? 0))
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.OrderUnit))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.ProductPriceFix != null ? src.ProductPriceFix.Amount : 0m))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.PriceLineAmount ?? 0));
        
        // Item mappings
        CreateMap<DocumentItem, OrderItemDTO>();
        CreateMap<DocumentItem, ReturnItemDTO>();
    }
}

// Placeholder DTOs - you'll need to create these
public class OrderItemDTO { }
public class ReturnItemDTO { }
