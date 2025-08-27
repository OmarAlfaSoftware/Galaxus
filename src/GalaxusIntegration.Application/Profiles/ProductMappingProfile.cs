using AutoMapper;
using GalaxusIntegration.Application.DTOs.Galaxus;
using GalaxusIntegration.Application.DTOs.Internal;

namespace GalaxusIntegration.Application.Profiles
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductDto, Stage1ProductDto>();
            CreateMap<ProductDto, Stage2CommercialDto>();
            CreateMap<ProductDto, Stage3SpecificationDto>();
            // Add reverse maps and custom mappings as needed
        }
    }
}