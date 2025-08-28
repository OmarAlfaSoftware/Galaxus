using GalaxusIntegration.Application.DTOs.ProductDTOs;
using GalaxusIntegration.Application.Interfaces;

namespace GalaxusIntegration.Application.ProductData.Strategies
{
    public class SpecificationDataStrategy : IProductDataStrategy
    {
        public async Task<IProductExcelData> GenerateProductData()
        {
            SpecificationDataDto specification = new SpecificationDataDto.SpecificationDataDtoBuilder()
                .WithProviderKey("p15")
                .WithKey("Farbe")
                .WithValue("Rot").Build();

            return new ProductDataGenerator
            {
                FileName = "specification_data.xlsx",
                Headers = specification.HeadList(),
                Data = new List<List<string>> { specification.DataToString() }
            };
        }
    }
}