using FluentValidation;
using Net.Architecture.Core.Constants;
using Net.Architecture.Entities.Dtos;

namespace Net.Architecture.Business.ValidationRules.FluentValidation
{
    public class RecoverPasswordDtoValidator : AbstractValidator<RecoverPasswordDto>
    {
        public RecoverPasswordDtoValidator()
        {
            RuleFor(l => l.EmailOrUsername).NotEmpty().WithMessage(ValidationMessages.RequiredEmailOrUsername);
        }
    }
}
