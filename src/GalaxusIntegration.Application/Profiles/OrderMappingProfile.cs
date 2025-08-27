using AutoMapper;
using GalaxusIntegration.Application.DTOs.Galaxus;
using GalaxusIntegration.Application.DTOs.Internal;

namespace GalaxusIntegration.Application.Profiles
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<OrderDto, GalaxusOrderDto>();
            CreateMap<GalaxusOrderDto, OrderDto>();
            CreateMap<OrderDto, OrderResponseDto>();
            // Add reverse maps and custom mappings as needed
        }
    }
}