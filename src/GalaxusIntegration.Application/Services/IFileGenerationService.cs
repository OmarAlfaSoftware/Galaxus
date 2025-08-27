namespace GalaxusIntegration.Application.Services
{
    public interface IFileGenerationService
    {
        Task<string> GenerateStage1FileAsync();
        Task<string> GenerateStage2FileAsync();
        Task<string> GenerateStage3FileAsync();
    }
}