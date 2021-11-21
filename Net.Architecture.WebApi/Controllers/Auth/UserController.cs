using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.Architecture.WebApi.Controllers;
using Pilates.Application.Business.Abstract.Auth;
using Pilates.Application.Business.ValidationRules.FluentValidation;
using Pilates.Application.Core.ActionsFilters;
using Pilates.Application.Core.Utilities.Result;
using Pilates.Application.Entities.Dtos;

namespace Pilates.Application.WebApi.Controllers.Auth
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
        [Authorize(Roles = "Company,Trainer,Admin,Demo")]
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
        [Authorize(Roles = "Company,Trainer,Admin")]
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
        [Authorize(Roles = "Company,Trainer,Admin")]
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
        [Authorize(Roles = "Company,Trainer,Admin,Demo")]
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
