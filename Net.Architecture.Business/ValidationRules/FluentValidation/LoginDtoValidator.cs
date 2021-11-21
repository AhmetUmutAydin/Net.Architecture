using FluentValidation;
using Net.Architecture.Core.Constants;
using Net.Architecture.Entities.Dtos;

namespace Net.Architecture.Business.ValidationRules.FluentValidation
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(l => l.Username).NotEmpty().WithMessage(ValidationMessages.RequiredUsername);
            RuleFor(l => l.Username).Length(6, 50).WithMessage(ValidationMessages.LengthUsername);

            RuleFor(l => l.Password).NotEmpty().WithMessage(ValidationMessages.RequiredPassword);
            RuleFor(l => l.Password).Length(5, 20).WithMessage(ValidationMessages.LengthPassword);

            RuleFor(l => l.ModuleRole).NotEmpty().WithMessage(ValidationMessages.RequiredModuleRole);
        }
    }
}
