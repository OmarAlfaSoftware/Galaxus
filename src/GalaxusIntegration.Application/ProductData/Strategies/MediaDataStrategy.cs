using GalaxusIntegration.Application.DTOs.ProductDTOs;
using GalaxusIntegration.Application.Interfaces;

namespace GalaxusIntegration.Application.ProductData.Strategies
{
    public class MediaDataStrategy : IProductDataStrategy
    {
        public async Task<IProductExcelData> GenerateProductData()
        {
            MediaDataDTO mediaDTO = new MediaDataDTO.MediaDataDTOBuilder()
                .WithProviderKey("P12")
                .AddImage("https://example.com/main_image.jpg")
                .AddImage("https://example.com/additional_image1.jpg")
                .AddImage("https://example.com/additional_image2.jpg")
                .AddProductLink("de", "https://example.com/product_de")
                .AddProductLink("en", "https://example.com/product_en")
                .Build();

            return new ProductDataGenerator
            {
                FileName = "media_data.xlsx",
                Headers = mediaDTO.HeadList(),
                Data = new List<List<string>>{mediaDTO.DataToString()} // To be populated from repository
            };
        }
    }
}