using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class EmailParameterValidator : AbstractValidator<EmailParameter>
    {
        public EmailParameterValidator()
        {
            RuleFor(p => p.SMTP).NotEmpty().WithMessage("SMTP boş olamaz");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Email boş olamaz");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Password boş olamaz");
            RuleFor(p => p.Port).NotEmpty().WithMessage("Port boş olamaz");
        }
    }
}
