using FluentValidation;
using Net.Architecture.Core.Constants;
using Net.Architecture.Entities.Dtos;

namespace Net.Architecture.Business.ValidationRules.FluentValidation
{
    public class UserProfileDtoValidator : AbstractValidator<UserProfileDto>
    {
        public UserProfileDtoValidator()
        {
            RuleFor(r => r.Username).NotEmpty().WithMessage(ValidationMessages.RequiredUsername);
            RuleFor(r => r.Username).Length(6, 50).WithMessage(ValidationMessages.LengthUsername);

            RuleFor(r => r.Email).NotEmpty().WithMessage(ValidationMessages.RequiredEmail);
            RuleFor(r => r.Email).MaximumLength(100).WithMessage(ValidationMessages.MaximumLengthEmail);
        }
    }
}
