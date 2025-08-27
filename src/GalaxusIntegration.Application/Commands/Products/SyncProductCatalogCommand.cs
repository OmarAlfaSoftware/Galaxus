using MediatR;

namespace GalaxusIntegration.Application.Commands.Products
{
    public class SyncProductCatalogCommand : IRequest<bool>
    {
        public SyncProductCatalogCommand() { }
    }
}