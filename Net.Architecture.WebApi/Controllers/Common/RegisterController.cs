using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Net.Architecture.WebApi.Controllers.Common
{
    [Route("api/v1/register")]
    [ApiController]
    public class RegisterController : BaseController
    {
        private readonly IRegisterService _registerManager;

        public RegisterController(IRegisterService registerManager)
        {
            _registerManager = registerManager;
        }

        [HttpPost]
        [DbTransaction]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IServiceResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post(StudioRegisterDto studioRegisterDto)
        {
            var result = await _registerManager.CreateRegister(studioRegisterDto);
            if (result.Result)
            {
                return NoContent();
            }
            return BadRequest(result.BadRequest());
        }
    }
}
