using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaxusIntegration.Application.DTOs.ProductDTOs;
using GalaxusIntegration.Application.Interfaces;

namespace GalaxusIntegration.Application.ProductData.Strategies
{
    public class PriceDataStrategy : IProductDataStrategy
    {
        public async Task<IProductExcelData> GenerateProductData()
        {
            var priceDTO = new PriceDataDTO.PriceDataDTOBuilder()
                .WithProviderKey("P12")
                .WithPriceWithoutVAT(100)
                .WithPriceWithVAT(107.7m)
                .WithVAT(7.7m)
                .Build();

            return new ProductDataGenerator
            {
                FileName = "price_data.xlsx",
                Headers = priceDTO.HeadList(),
                Data = new List<List<string>>
                {
                    priceDTO.DataToString()
                }
            };
        }
    }
}
