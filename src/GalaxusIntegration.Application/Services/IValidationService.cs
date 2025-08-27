namespace GalaxusIntegration.Application.Services
{
    public interface IValidationService
    {
        Task<bool> ValidateStage1ProductAsync(object dto);
        Task<bool> ValidateStage2CommercialAsync(object dto);
        Task<bool> ValidateGalaxusOrderAsync(object dto);
        Task<bool> ValidateProviderKeyFormatAsync(string providerKey);
    }
}