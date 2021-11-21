using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Net.Architecture.Business.Abstract;
using Net.Architecture.Business.Abstract.Auth;
using Net.Architecture.Business.Helpers.Abstract;
using Net.Architecture.Core.Constants;
using Net.Architecture.Core.Extensions;
using Net.Architecture.Core.Utilities.Generator;
using Net.Architecture.Core.Utilities.Result;
using Net.Architecture.Core.Utilities.Security.Hashing;
using Net.Architecture.DataAccess.Concrete.Auth;
using Net.Architecture.DataAccess.UnitOfWork;
using Net.Architecture.Entities.Concrete.Auth;
using Net.Architecture.Entities.Configurations;
using Net.Architecture.Entities.Dtos;
using Net.Architecture.Entities.Views;

namespace Net.Architecture.Business.Concrete.Auth
{
    public class AuthManager : BaseBusiness, IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenHelper _tokenHelper;
        private readonly IRoleHelper _roleHelper;
        private readonly List<Client> _clients;
        private readonly IEmailHelper _emailHelper;

        public AuthManager(IUnitOfWork unitOfWork, ITokenHelper tokenHelper, IOptions<List<Client>> optionsClient, IRoleHelper roleHelper, IEmailHelper emailHelper)
        {
            _unitOfWork = unitOfWork;
            _tokenHelper = tokenHelper;
            _roleHelper = roleHelper;
            _emailHelper = emailHelper;
            _clients = optionsClient.Value;
        }

        public async Task<IServiceResult<User>> Register(RegisterDto registerDto)
        {
            var isEmailExists = await _unitOfWork.Repository<User>().AnyAsync(u => u.Email == registerDto.Email);
            if (isEmailExists)
            {
                return new ServiceResult<User>(Messages.EmailExists, false);
            }

            var isUserExists = await _unitOfWork.Repository<User>().AnyAsync(u => u.Username == registerDto.Username);
            if (isUserExists)
            {
                return new ServiceResult<User>(Messages.UsernameExists, false);
            }

            byte[] passwordHash;
            byte[] passwordSalt;
            HashingHelper.CreatePasswordHash(registerDto.Password, out passwordHash, out passwordSalt);

            User user = registerDto.ToEntity<User>();
            user.Status = true;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _unitOfWork.Repository<User>().AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
            await _roleHelper.SaveUserRole(user.Id);

            return new ServiceResult<User>(user);
        }

        public async Task<IServiceResult<TokenDto>> CreateToken(User user)
        {
            var roles = await _unitOfWork.CustomRepository<UserRoleRepository>().GetUserRoles(user.Id);
            var token = _tokenHelper.CreateToken(user, roles);
            await SaveRefreshToken(user.Id, token);
            return new ServiceResult<TokenDto>(token);
        }

        public async Task<IServiceResult<User>> Login(LoginDto loginDto)
        {
            var user = await _unitOfWork.Repository<User>().GetAsync(u => (u.Username == loginDto.Username || u.Email == loginDto.Username) && u.Status);

            if (user is null)
            {
                return new ServiceResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new ServiceResult<User>(Messages.WrongPassword);
            }

            return new ServiceResult<User>(user);
        }

        public IServiceResult<AccessTokenDto> ClientLogin(ClientLoginDto clientLoginDto)
        {
            var client = _clients.SingleOrDefault(x => x.Id == clientLoginDto.ClientId && x.Secret == clientLoginDto.ClientSecret);
            if (client is null)
            {
                return new ServiceResult<AccessTokenDto>(Messages.ClientLoginFailed);
            }
            var token = _tokenHelper.CreateTokenForClient(client);

            return new ServiceResult<AccessTokenDto>(token);
        }

        public async Task<IServiceResult<User>> GetUserByRefreshToken(string refreshToken)
        {
            var user = await _unitOfWork.Repository<User>().SingleOrDefaultAsync(x => x.UserRefreshToken.Code == refreshToken && x.Status && x.UserRefreshToken.Status);
            if (user is null)
            {
                return new ServiceResult<User>(Messages.WrongRefreshToken);
            }
            return new ServiceResult<User>(user);
        }

        public async Task<IServiceResult> RemoveRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _unitOfWork.Repository<UserRefreshToken>().GetAsync(x => x.Code == refreshToken && x.Status);
            if (existRefreshToken is null)
            {
                return new ServiceResult<User>(Messages.WrongRefreshToken);
            }
            _unitOfWork.Repository<UserRefreshToken>().Delete(existRefreshToken);
            await _unitOfWork.SaveChangesAsync();
            return new ServiceResult();
        }

        public async Task<IServiceResult<RecoverPasswordView>> RecoverSavePassword(RecoverPasswordDto recoverPasswordDto)
        {
            var user = await _unitOfWork.Repository<User>().GetAsync(u => u.Status && (u.Username == recoverPasswordDto.EmailOrUsername || u.Email == recoverPasswordDto.EmailOrUsername));
            if (user is null)
                return new ServiceResult<RecoverPasswordView>(Messages.UserNotFound);

            RecoverPasswordView recoverPasswordView = new RecoverPasswordView
            {
                NewPassword = RandomStringGenerator.RandomString(8),
                Email = user.Email
            };

            byte[] passwordHash;
            byte[] passwordSalt;
            HashingHelper.CreatePasswordHash(recoverPasswordView.NewPassword, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _unitOfWork.Repository<User>().Update(user);
            await _unitOfWork.SaveChangesAsync();

            return new ServiceResult<RecoverPasswordView>(recoverPasswordView);
        }

        public IServiceResult SendRecoverPassword(RecoverPasswordView recoverPasswordView)
        {
            var mailContent = new MailContentView()
            {
                Body = $"E-posta: {recoverPasswordView.Email}<br/>Şifre: {recoverPasswordView.NewPassword}",
                Title = "Şifreniz sıfırlandı.",
                ToList = new List<string>()
                {
                    recoverPasswordView.Email
                }
            };

            _emailHelper.SendMailWithDefaultOutlook(mailContent);
            return new ServiceResult();
        }

        private async Task SaveRefreshToken(long userId, TokenDto token)
        {
            var userRefreshToken = await _unitOfWork.Repository<UserRefreshToken>().GetAsync(x => x.UserId == userId && x.Status);
            if (userRefreshToken is null)
            {
                userRefreshToken = new UserRefreshToken()
                {
                    Code = token.RefreshToken,
                    Expiration = token.RefreshTokenExpiration,
                    Status = true,
                    UserId = userId,
                };
                await _unitOfWork.Repository<UserRefreshToken>().AddAsync(userRefreshToken);
            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
                userRefreshToken.Status = true;
                _unitOfWork.Repository<UserRefreshToken>().Update(userRefreshToken);
            }
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
