using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using Net.Architecture.Core.Constants;
using Net.Architecture.Core.CrossCuttingConcerns.Validation;


namespace Net.Architecture.Core.ActionsFilters
{
    public class Validation : ActionFilterAttribute
    {
        private readonly Type _validatorType;

        public Validation(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception(Messages.WrongValidationType);
            }
            _validatorType = validatorType;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType?.GetGenericArguments()[0];
            var entities = context.ActionArguments.Where(t => t.Value.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity.Value);
            }
            await next();
        }
    }
}
