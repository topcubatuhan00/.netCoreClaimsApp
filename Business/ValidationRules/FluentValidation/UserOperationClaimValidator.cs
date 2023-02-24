using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserOperationClaimValidator : AbstractValidator<UserOperationClaim>
    {
        public UserOperationClaimValidator()
        {
            RuleFor(p => p.UserId).NotEmpty().WithMessage("Yetki Ataması Için Kullanıcı Seçimi Zorunludur.");
            RuleFor(p => p.OperationClaimId).NotEmpty().WithMessage("Yetki Ataması Için Yetki Seçimi Zorunludur.");
        }
    }
}
