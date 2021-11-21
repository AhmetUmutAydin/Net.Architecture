using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Net.Architecture.WebApi.Controllers;
using Pilates.Application.Business.Helpers.Concrete;
using Pilates.Application.Core.Utilities.Result;
using Pilates.Application.Entities.Dtos;
using Pilates.Application.Business.ValidationRules.FluentValidation;
using Pilates.Application.Core.ActionsFilters;
using Pilates.Application.Entities.Enums;

namespace Pilates.Application.WebApi.Controllers.Common
{
    [Route("api/v1/file")]
    [ApiController]
    public class FileController : BaseController
    {
        [HttpPost("public/upload")]
        [Authorize(Roles = "Company,Trainer")]
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
