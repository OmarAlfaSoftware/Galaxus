using FluentValidation;
using GalaxusIntegration.Application.DTOs.Galaxus;

namespace GalaxusIntegration.Application.Validators
{
    public class Stage2CommercialValidator : AbstractValidator<Stage2CommercialDto>
    {
        public Stage2CommercialValidator()
        {
            RuleFor(x => x.ProviderKey).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.StockQuantity).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Currency).Equal("CHF");
            RuleFor(x => x.VAT_Rate).Equal(7.7m);
        }
    }
}