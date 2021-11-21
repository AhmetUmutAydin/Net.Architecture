using FluentValidation;
using Net.Architecture.Core.Constants;
using Net.Architecture.Entities.Dtos;

namespace Net.Architecture.Business.ValidationRules.FluentValidation
{
    public class CommunicationDtoValidator : AbstractValidator<CommunicationDto>
    {
        public CommunicationDtoValidator()
        {
            RuleFor(c => c.Value).NotEmpty().WithMessage(ValidationMessages.RequiredCommunicationValue);
            RuleFor(c => c.Value).MaximumLength(100).WithMessage(ValidationMessages.MaximumLengthCommunicationValue);
            RuleFor(c => c.CommunicationType).NotEmpty().WithMessage(ValidationMessages.RequiredCommunicationType);

        }
    }
}
