using AutoMapper;
using GalaxusIntegration.Application.DTOs.Galaxus;
using GalaxusIntegration.Application.DTOs.Internal;

namespace GalaxusIntegration.Application.Profiles
{
    public class GalaxusFileFormatProfile : Profile
    {
        public GalaxusFileFormatProfile()
        {
            // Map internal models to Galaxus file formats
            CreateMap<ProductDto, Stage1ProductDto>();
            CreateMap<ProductDto, Stage2CommercialDto>();
            CreateMap<ProductDto, Stage3SpecificationDto>();
            // Add more mappings as needed
        }
    }
}