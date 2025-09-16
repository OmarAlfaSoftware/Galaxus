using MediatR;

namespace GalaxusIntegration.Application.Commands.Products
{
    public class UpdateProductPriceCommand : IRequest<bool>
    {
        public string ProviderKey { get; set; }
        public double NewPrice { get; set; }
        public UpdateProductPriceCommand(string providerKey, double newPrice)
        {
            ProviderKey = providerKey;
            NewPrice = newPrice;
        }
    }
}