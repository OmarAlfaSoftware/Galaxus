using GalaxusIntegration.Application.DTOs.ProductDTOs;
using GalaxusIntegration.Application.Interfaces;

namespace GalaxusIntegration.Application.ProductData.Strategies
{
    public class ProductAccessoryDataStrategy : IProductDataStrategy
    {
        public async Task<IProductExcelData> GenerateProductData()
        {
            ProductAccessoryDataDTO accessory = new ProductAccessoryDataDTO.ProductAccessoryDataDTOBuilder().WithAccessoryProviderKey("p12")
                .WithProviderKey("p15").Build();


            return new ProductDataGenerator
            {
                FileName = "accessory_data.xlsx",
                Headers = accessory.HeadList(),
                Data = new List<List<string>> { accessory.DataToString() }
            };
        }


    }
}