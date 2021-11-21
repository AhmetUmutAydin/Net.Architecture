using FluentValidation;
using Net.Architecture.Core.Constants;
using Net.Architecture.Entities.Dtos;

namespace Net.Architecture.Business.ValidationRules.FluentValidation
{
    public class AddressDtoValidator : AbstractValidator<AddressDto>
    {
        public AddressDtoValidator()
        {
            RuleFor(a => a.CityId).NotEmpty().WithMessage(ValidationMessages.RequiredCity);

            RuleFor(a => a.Description).NotEmpty().WithMessage(ValidationMessages.RequiredAddressDescription);
            RuleFor(a => a.Description).MaximumLength(200).WithMessage(ValidationMessages.MaximumLengthAddressDescription);
        }
    }
}
