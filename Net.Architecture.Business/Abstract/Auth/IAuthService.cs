using System;
using System.Threading.Tasks;
using Net.Architecture.Core.Utilities.Result;
using Net.Architecture.Entities.Concrete.Auth;
using Net.Architecture.Entities.Dtos;
using Net.Architecture.Entities.Views;

namespace Net.Architecture.Business.Abstract.Auth
{
    public interface IAuthService
    {
        Task<IServiceResult<User>> Register(RegisterDto registerDto);
        Task<IServiceResult<TokenDto>> CreateToken(User user);
        Task<IServiceResult<User>> Login(LoginDto loginDto);
        IServiceResult<AccessTokenDto> ClientLogin(ClientLoginDto clientLoginDto);
        Task<IServiceResult<User>> GetUserByRefreshToken(string refreshToken);
        Task<IServiceResult> RemoveRefreshToken(string refreshToken);
        Task<IServiceResult<RecoverPasswordView>> RecoverSavePassword(RecoverPasswordDto recoverPasswordDto);
        IServiceResult SendRecoverPassword(RecoverPasswordView recoverPasswordView);
    }
}
