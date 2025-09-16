using MediatR;

namespace GalaxusIntegration.Application.Commands.Products
{
    public class GenerateStage2FileCommand : IRequest<string>
    {
        public GenerateStage2FileCommand() { }
    }
}