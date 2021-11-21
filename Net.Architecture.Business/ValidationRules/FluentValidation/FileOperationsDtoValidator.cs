using FluentValidation;
using Net.Architecture.Core.Constants;
using Net.Architecture.Entities.Dtos;

namespace Net.Architecture.Business.ValidationRules.FluentValidation
{
    public class FileOperationsDtoValidator : AbstractValidator<FileOperationsDto>
    {
        public FileOperationsDtoValidator()
        {
            RuleFor(a => a.File).NotEmpty().WithMessage(ValidationMessages.RequiredFile);
            RuleFor(a => a.File.FormFile).NotEmpty().WithMessage(ValidationMessages.RequiredFile);
        }
    }
}
