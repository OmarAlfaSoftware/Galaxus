using MediatR;

namespace GalaxusIntegration.Application.Commands.Products
{
    public class UpdateProductPriceCommand : IRequest<bool>
    {
        public string ProviderKey { get; set; }
        public decimal NewPrice { get; set; }
        public UpdateProductPriceCommand(string providerKey, decimal newPrice)
        {
            ProviderKey = providerKey;
            NewPrice = newPrice;
        }
    }
}