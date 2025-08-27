using FluentValidation;

namespace GalaxusIntegration.Application.Validators
{
    public class ProviderKeyFormatValidator : AbstractValidator<string>
    {
        public ProviderKeyFormatValidator()
        {
            RuleFor(x => x).NotEmpty().MaximumLength(100).Matches("^[\x20-\x7E]+$");
        }
    }
}