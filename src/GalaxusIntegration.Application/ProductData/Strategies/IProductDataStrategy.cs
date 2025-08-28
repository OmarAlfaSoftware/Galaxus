using GalaxusIntegration.Application.Interfaces;

namespace GalaxusIntegration.Application.ProductData.Strategies
{
    public interface IProductDataStrategy
    {
        Task<IProductExcelData> GenerateProductData();
    }
}