using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Net.Architecture.Core.Utilities.IoC;
using Net.Architecture.DataAccess.UnitOfWork;

namespace Net.Architecture.Core.ActionsFilters
{
    public class DbTransaction : ActionFilterAttribute
    {
        private IUnitOfWork _unitOfWork;
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _unitOfWork = IocManager.Resolve<IUnitOfWork>();
            await _unitOfWork.CheckTransactionAsync();
            var response = await next();
            var result = response.Result;
            if (result is OkObjectResult || result is NoContentResult || result is CreatedAtActionResult)
            {
                await _unitOfWork.CommitAsync();
            }
            else
            {
                await _unitOfWork.RollbackAsync();
            }
        }
    }
}
