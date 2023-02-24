using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Kullanıcı Adı Boş Olamaz");
            RuleFor(p => p.ImageUrl).NotEmpty().WithMessage("Profil Resmi Boş Olamaz");
        }
    }
}
