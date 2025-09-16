using GalaxusIntegration.Application.DTOs.ProductDTOs;
using GalaxusIntegration.Application.Interfaces;

namespace GalaxusIntegration.Application.ProductData.Strategies
{
    public class ProductDataStrategy : IProductDataStrategy
    {
        public async Task<IProductExcelData> GenerateProductData()
        {
            var PDTDO = new ProudctDataDTO.ProductDataDTOBuilder()
                .WithBrandName("abc")
                .WithCategoryGroup_1("cat1")
                .WithCategoryGroup_2("cat2")
                .WithCategoryGroup_3("cat3")
                .WithCountryOfOrigin("CH")
                .WithGTIN("1234567890123")
                .WithHeight_m(0.5)
                .WithLength_m(1.0)
                .WithWeight_g(1500)
                .WithWidth_m(0.3)
                .WithManufacturerKey("manu123")
                .WithProductCategory("Electronics")
                .WithProviderKey("prov")
                .WithReleaseDate_CH(DateTime.Now)
                .WithTARICCode("TARIC123")
                .WithTARESCode("TARES123")
                .WithVariantName("Standard")
                .AddProductTitleInWithLanguage("de","P12")
                .AddProductTitleInWithLanguage("en","P12")
                .AddProductDescriptionInWithLanguage("de", "Dies ist eine lange Beschreibung des Produk")
                .Build();

            return new ProductDataGenerator
            {
                FileName = "product_data.xlsx",
                Headers = PDTDO.HeadList(),
                Data = new List<List<string>>
                {
                    PDTDO.DataToString()
                } // To be populated from repository
            };
        }

     
    }
}