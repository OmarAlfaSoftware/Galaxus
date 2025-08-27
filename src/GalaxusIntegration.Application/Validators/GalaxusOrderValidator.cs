using FluentValidation;
using GalaxusIntegration.Application.DTOs.Galaxus;

namespace GalaxusIntegration.Application.Validators
{
    public class GalaxusOrderValidator : AbstractValidator<GalaxusOrderDto>
    {
        public GalaxusOrderValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty();
            RuleFor(x => x.CustomerName).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
        }
    }
}