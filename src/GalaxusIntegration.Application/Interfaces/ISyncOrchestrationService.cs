namespace GalaxusIntegration.Application.Interfaces
{
    public interface ISyncOrchestrationService
    {
        Task<bool> SyncProductCatalogAsync();
        Task<bool> SyncOrdersAsync();
        Task<bool> SyncFilesAsync();
    }
}