using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Net.Architecture.WebApi.Controllers.Common
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
