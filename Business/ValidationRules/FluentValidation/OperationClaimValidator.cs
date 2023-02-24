using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class OperationClaimValidator : AbstractValidator<OperationClaim>
    {
        public OperationClaimValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Yetki Alanı Boş Olamaz");
        }
    }
}
