namespace GalaxusIntegration.Application.Services
{
    public interface ISyncOrchestrationService
    {
        Task<bool> SyncProductCatalogAsync();
        Task<bool> SyncOrdersAsync();
        Task<bool> SyncFilesAsync();
    }
}