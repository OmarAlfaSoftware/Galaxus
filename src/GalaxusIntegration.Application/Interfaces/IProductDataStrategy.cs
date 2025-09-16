namespace GalaxusIntegration.Application.Interfaces
{
    public interface IProductDataStrategy
    {
        Task<IProductExcelData> GenerateProductData();
    }
}