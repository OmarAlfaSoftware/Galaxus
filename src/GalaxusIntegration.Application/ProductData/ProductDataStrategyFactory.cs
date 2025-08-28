using GalaxusIntegration.Application.ProductData.Strategies;
using GalaxusIntegration.Shared.Enum;

namespace GalaxusIntegration.Application.ProductData
{
    public class ProductDataStrategyFactory
    {
        public static IProductDataStrategy CreateStrategy(ProductDataType type)
        {
            return type switch
            {
                ProductDataType.ProductData => new ProductDataStrategy(),
                ProductDataType.MediaData => new MediaDataStrategy(),
                ProductDataType.AccessoryData => new ProductAccessoryDataStrategy(),
                ProductDataType.StockData => new StockDataStrategy(),
                ProductDataType.PriceData => new PriceDataStrategy(),
                _ => throw new ArgumentException($"Unknown product data type: {type}")
            };
        }
    }
}