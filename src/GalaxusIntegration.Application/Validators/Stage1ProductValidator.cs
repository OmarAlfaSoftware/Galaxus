using FluentValidation;
using GalaxusIntegration.Application.DTOs.Galaxus;

namespace GalaxusIntegration.Application.Validators
{
    public class Stage1ProductValidator : AbstractValidator<Stage1ProductDto>
    {
        public Stage1ProductValidator()
        {
            RuleFor(x => x.ProviderKey).NotEmpty().MaximumLength(100).Matches("^[\x20-\x7E]+$");
            RuleFor(x => x.Gtin).NotEmpty();
            RuleFor(x => x.BrandName).NotEmpty();
            RuleFor(x => x.ProductTitle_de).NotEmpty().MaximumLength(100);
            RuleFor(x => x.LongDescription_de).MaximumLength(4000);
            RuleFor(x => x.MainImageUrl).NotEmpty().MaximumLength(300).Must(url => url.StartsWith("https://"));
        }
    }
}