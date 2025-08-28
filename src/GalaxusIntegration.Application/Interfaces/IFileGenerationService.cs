namespace GalaxusIntegration.Application.Interfaces
{
    public interface IFileGenerationService
    {
        public Task<string> GenerateExcelFile(IProductExcelData Data);
    }
}