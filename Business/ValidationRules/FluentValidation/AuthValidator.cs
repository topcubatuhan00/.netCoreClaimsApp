using Entities.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class AuthValidator : AbstractValidator<RegisterAuthDto>
    {
        public AuthValidator()
        {
            RuleFor(p => p.name).NotEmpty().WithMessage("Kullanıcı Adı Boş Olamaz");
            RuleFor(p => p.image.FileName).NotEmpty().WithMessage("Resim Boş Olamaz");
            RuleFor(p => p.password).NotEmpty().WithMessage("Şifre Boş Olamaz");
            RuleFor(p => p.password).MinimumLength(6).WithMessage("Şifre En Az 6 Karakter Olamaz");
        }
    }
}
