using MediatR;

namespace GalaxusIntegration.Application.Commands.Products
{
    public class GenerateStage1FileCommand : IRequest<string>
    {
        public GenerateStage1FileCommand() { }
    }
}