using MediatR;

namespace GalaxusIntegration.Application.Commands.Products
{
    public class GenerateStage3FileCommand : IRequest<string>
    {
        public GenerateStage3FileCommand() { }
    }
}