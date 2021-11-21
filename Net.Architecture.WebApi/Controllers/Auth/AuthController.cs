using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.Architecture.Business.Abstract.Auth;
using Net.Architecture.Business.ValidationRules.FluentValidation;
using Net.Architecture.Core.ActionsFilters;
using Net.Architecture.Core.Utilities.Result;
using Net.Architecture.Entities.Dtos;

namespace Net.Architecture.WebApi.Controllers.Auth
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("register")]
        [DbTransaction]
        [Validation(typeof(RegisterDtoValidator))]
        [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IServiceResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            var registerResult = await _authService.Register(registerDto);
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
