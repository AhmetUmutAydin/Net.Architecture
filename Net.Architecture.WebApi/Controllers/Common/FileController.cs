using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.Architecture.Business.Helpers.Concrete;
using Net.Architecture.Business.ValidationRules.FluentValidation;
using Net.Architecture.Core.ActionsFilters;
using Net.Architecture.Core.Utilities.Result;
using Net.Architecture.Entities.Dtos;

namespace Net.Architecture.WebApi.Controllers.Common
{
    [Route("api/v1/file")]
    [ApiController]
    public class FileController : BaseController
    {
        [HttpPost("public/upload")]
        [Authorize(Roles = "")]
        [DbTransaction]
        [Validation(typeof(FileOperationsDtoValidator))]
        [ProducesResponseType(typeof(FileDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IServiceResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FileDto>> SaveFileToRoot([FromForm] FileOperationsDto fileOperationsDto)
        {
            var result = await FileFactory.CreateInstance(fileOperationsDto.Decider.DeciderType).SaveFileToRoot(fileOperationsDto);
            if (result.Result)
                return Ok(result.Data);
            else
                return BadRequest(result.BadRequest());
        }
    }
}
