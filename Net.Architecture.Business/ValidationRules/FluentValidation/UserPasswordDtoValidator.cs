using FluentValidation;
using Net.Architecture.Core.Constants;
using Net.Architecture.Entities.Dtos;

namespace Net.Architecture.Business.ValidationRules.FluentValidation
{
    public class UserPasswordDtoValidator : AbstractValidator<UserProfileDto>
    {
        public UserPasswordDtoValidator()
        {
            RuleFor(r => r.NewPassword).NotEmpty().WithMessage(ValidationMessages.RequiredPassword);
            RuleFor(r => r.NewPassword).Length(5, 50).WithMessage(ValidationMessages.LengthPassword);
        }
    }
}
