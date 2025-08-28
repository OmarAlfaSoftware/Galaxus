using GalaxusIntegration.Application.DTOs.ProductDTOs;
using GalaxusIntegration.Application.Interfaces;

namespace GalaxusIntegration.Application.ProductData.Strategies
{
    public class StockDataStrategy : IProductDataStrategy
    {
        public async Task<IProductExcelData> GenerateProductData()
        {

            var stockData = new StockDataDto.StockDataDtoBuilder()
                .WithDirectDeliverySupported(true)
                .WithLogisticUnit(10)
                .WithMinimumOrderQuantity(1)
                .WithRestockDate(DateTime.Parse("01-25-2025"))
                .WithOrderQuantitySteps(1)
                .WithProviderKey("prov")
                .WithWarehouseCountry("CH")
                .WithQuantityOnStock(50)
                .WithTradeUnit(1)
                
                .Build();

            return new ProductDataGenerator
            {
                FileName = "stock_data.xlsx",
                Headers = stockData.HeadList(),
                Data = new List<List<string>> { stockData.DataToString() }
            };

        }
    }
}