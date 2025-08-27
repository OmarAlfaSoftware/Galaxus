using MediatR;
using GalaxusIntegration.Application.DTOs.Internal;

namespace GalaxusIntegration.Application.Commands.Products
{
    public class CreateProductCommand : IRequest<ProductDto>
    {
        public ProductDto Product { get; set; }
        public CreateProductCommand(ProductDto product)
        {
            Product = product;
        }
    }
}