using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.Architecture.Business.Abstract.Auth;
using Net.Architecture.Core.ActionsFilters;
using Net.Architecture.Core.Utilities.Result;
using Net.Architecture.Entities.Dtos;

namespace Net.Architecture.WebApi.Controllers.Auth
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userManager;

        public UserController(IUserService userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "")]
        [ProducesResponseType(typeof(UserProfileDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IServiceResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserProfileDto>> Get()
        {
            var result = await _userManager.GetUserProfile();
            if (result.Result)
                return Ok(result.Data);
            else
                return BadRequest(result.BadRequest());
        }

        [HttpPut("profile")]
        [Authorize(Roles = "")]
        [Validation(typeof(UserProfileDtoValidator))]
        [DbTransaction]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IServiceResult), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PutUserProfile(UserProfileDto userProfileDto)
        {
            var result = await _userManager.SaveUserProfile(userProfileDto);
            if (result.Result)
                return NoContent();
            else
                return NotFound(result.NotFound());
        }


        [HttpPut("password")]
        [Authorize(Roles = "")]
        [Validation(typeof(UserPasswordDtoValidator))]
        [DbTransaction]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IServiceResult), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PutPassword(UserProfileDto userProfileDto)
        {
            var result = await _userManager.SaveUserPassword(userProfileDto);
            if (result.Result)
                return NoContent();
            else
                return NotFound(result.NotFound());
        }


        [HttpGet("layout")]
        [Authorize(Roles = "")]
        [ProducesResponseType(typeof(LayoutDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IServiceResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LayoutDto>> GetUserLayout()
        {
            var result = await _userManager.GetUserLayout();
            if (result.Result)
                return Ok(result.Data);
            else
                return BadRequest(result.BadRequest());
        }
    }
}
