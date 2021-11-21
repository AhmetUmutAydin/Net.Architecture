using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.Architecture.WebApi.Controllers;
using Pilates.Application.Business.Abstract.Auth;
using Pilates.Application.Business.Abstract.Studio;
using Pilates.Application.Business.ValidationRules.FluentValidation;
using Pilates.Application.Core.ActionsFilters;
using Pilates.Application.Core.Utilities.Result;
using Pilates.Application.Entities.Configurations.JsonObjects;
using Pilates.Application.Entities.Dtos;

namespace Pilates.Application.WebApi.Controllers.Auth
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IInvitationService _invitationService;

        public AuthController(IAuthService authService, IInvitationService invitationService)
        {
            _authService = authService;
            _invitationService = invitationService;
        }

        [HttpGet("welcome")]
        public async Task<ActionResult> Welcome(string refreshToken)
        {
            await _authService.welcome(refreshToken);
            return Ok(DateTime.Now.ToShortTimeString());
        }

        [HttpPost("register")]
        [DbTransaction]
        [Validation(typeof(RegisterDtoValidator))]
        [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IServiceResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            var invationResult = await _invitationService.GetInvitationValueAndUpdateStatus<InvitationRegisterValue>(registerDto.HashedEmployee);
            if (!invationResult.Result)
            {
                return BadRequest(invationResult.BadRequest());
            }

            var registerResult = await _authService.Register(registerDto, invationResult.Data.EmployeeId);
            if (!registerResult.Result)
            {
                return BadRequest(registerResult.BadRequest());
            }

            var result = await _authService.CreateToken(registerResult.Data);
            if (result.Result)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.BadRequest());

        }

        [HttpGet("register/{guid}")]
        [ProducesResponseType(typeof(RegisterInformationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IServiceResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Register(Guid guid)
        {
            var result = await _authService.GetRegisterInformation(guid);
            if (result.Result)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.BadRequest());
        }

        [HttpPost("login")]
        [DbTransaction]
        [Validation(typeof(LoginDtoValidator))]
        [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IServiceResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            var userResult = await _authService.Login(loginDto);
            if (!userResult.Result)
            {
                return BadRequest(userResult.BadRequest());
            }
            var result = await _authService.CreateToken(userResult.Data);
            if (result.Result)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.BadRequest());
        }

        [HttpPost("client-login")]
        [ProducesResponseType(typeof(AccessTokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IServiceResult), StatusCodes.Status400BadRequest)]
        public ActionResult ClientLogin(ClientLoginDto clientLoginDto)
        {
            var result = _authService.ClientLogin(clientLoginDto);
            if (result.Result)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.BadRequest());
        }

        [HttpGet("refresh-token/{token}")]
        [DbTransaction]
        [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IServiceResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateTokenByRefreshToken(string token)
        {
            var userResult = await _authService.GetUserByRefreshToken(token);
            if (!userResult.Result)
            {
                return BadRequest(userResult.BadRequest());
            }
            var result = await _authService.CreateToken(userResult.Data);
            if (result.Result)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.BadRequest());
        }

        [HttpPost("revoke-refresh-token")]
        [DbTransaction]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IServiceResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RevokeRefreshToken(string refreshToken)
        {
            var result = await _authService.RemoveRefreshToken(refreshToken);
            if (result.Result)
            {
                return NoContent();
            }
            return BadRequest(result.BadRequest());
        }

        [HttpPost("recover-password")]
        [DbTransaction]
        [Validation(typeof(RecoverPasswordDtoValidator))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IServiceResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RecoverPassword(RecoverPasswordDto recoverPasswordDto)
        {
            var recoverPasswordResult = await _authService.RecoverSavePassword(recoverPasswordDto);
            if (recoverPasswordResult.Result)
            {
                var result = _authService.SendRecoverPassword(recoverPasswordResult.Data);
                if (result.Result)
                {
                    return NoContent();
                }
            }
            return BadRequest(recoverPasswordResult);
        }
    }
}
