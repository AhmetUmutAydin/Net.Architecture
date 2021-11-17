using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Net.Architecture.Business.Abstract;
using Net.Architecture.Business.Abstract.Auth;
using Net.Architecture.Business.Helpers.Abstract;
using Net.Architecture.Core.Constants;
using Net.Architecture.Core.CrossCuttingConcerns.Caching;
using Net.Architecture.Core.Utilities.Generator;
using Net.Architecture.Core.Utilities.Result;
using Net.Architecture.Core.Utilities.Security.Hashing;
using Net.Architecture.DataAccess.UnitOfWork;
using Net.Architecture.Entities.Concrete.Auth;
using Net.Architecture.Entities.Configurations;
using Net.Architecture.Entities.Dtos;
using Net.Architecture.Entities.Enums;
using Net.Architecture.Entities.Views;
using Newtonsoft.Json;

namespace Net.Architecture.Business.Concrete.Auth
{
    public class AuthManager : BaseBusiness, IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenHelper _tokenHelper;
        private readonly IRoleHelper _roleHelper;
        private readonly List<Client> _clients;
        private readonly IEmailHelper _emailHelper;
        private readonly ICacheManager _cacheManager;

        public AuthManager(IUnitOfWork unitOfWork, ITokenHelper tokenHelper, IOptions<List<Client>> optionsClient, IRoleHelper roleHelper, IEmailHelper emailHelper, ICacheManager cacheManager)
        {
            _unitOfWork = unitOfWork;
            _tokenHelper = tokenHelper;
            _roleHelper = roleHelper;
            _emailHelper = emailHelper;
            _clients = optionsClient.Value;
            _cacheManager = cacheManager;
        }

        public async Task<IServiceResult<User>> Register(RegisterDto registerDto, long employeeId)
        {
            var isEmployeeExists = await _unitOfWork.Repository<EmployeeDal>().GetAsync(x => x.Status && x.Id == employeeId);
            if (isEmployeeExists is null)
            {
                return new ServiceResult<User>(Messages.EmployeeNotFound, false);
            }

            var isEmailExists = await _unitOfWork.Repository<UserDal>().CheckEmailExists(registerDto.Email);
            if (isEmailExists)
            {
                return new ServiceResult<User>(Messages.EmailExists, false);
            }

            var isUserExists = await _unitOfWork.Repository<UserDal>().CheckUsernameExists(registerDto.Username);
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
            user.EmployeeId = employeeId;
            user.Employee = isEmployeeExists;

            await _unitOfWork.Repository<UserDal>().AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
            await _roleHelper.SaveUserRole(user.Id, user.Employee.EmployeeType);

            return new ServiceResult<User>(user);
        }

        public async Task<IServiceResult<RegisterInformationDto>> GetRegisterInformation(Guid guid)
        {
            var invitation = await _unitOfWork.Repository<InvitationDal>().GetInvitation(guid);
            if (invitation is null)
                return new ServiceResult<RegisterInformationDto>(Messages.WrongInvitation);

            if (invitation.ExpirationDate < DateTime.Now)
                return new ServiceResult<RegisterInformationDto>(Messages.InvitationExpired);

            InvitationRegisterValue invitationValue = JsonConvert.DeserializeObject<InvitationRegisterValue>(invitation.Value);

            var registerInformation = await _unitOfWork.Repository<EmployeeDal>().GetEmployeeRegisterInformation(invitationValue.EmployeeId);
            if (invitation.CommunicationType == (long)Enums.CommunicationType.Email)
                registerInformation.Email = invitation.CommunicationValue;

            return new ServiceResult<RegisterInformationDto>(registerInformation);
        }

        public async Task<IServiceResult<TokenDto>> CreateToken(User user)
        {
            var roles = await _unitOfWork.Repository<UserRoleDal>().GetUserRoles(user.Id);
            var token = _tokenHelper.CreateToken(user, roles);
            await SaveRefreshToken(user.Id, token);
            return new ServiceResult<TokenDto>(token);
        }

        public async Task<IServiceResult<User>> Login(LoginDto loginDto)
        {
            var user = await _unitOfWork.Repository<UserDal>().GetUserWithEmployee(loginDto.Username, loginDto.ModuleRole);

            if (user is null)
            {
                return new ServiceResult<User>(Messages.UserNotFound);
            }
            if (!HashingHelper.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new ServiceResult<User>(Messages.WrongPassword);
            }

            var membership = (await _cacheManager.GetEntities<Membership>()).FirstOrDefault(x => x.Status && x.InstitutionId == user.Employee.InstitutionId);

            if (membership == null || membership.ExpiredDate < DateTime.Now)
                return new ServiceResult<User>(Messages.MembershipExpired);

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
            var user = await _unitOfWork.Repository<UserRefreshTokenDal>().GetUserByRefreshToken(refreshToken);

            if (user is null)
            {
                return new ServiceResult<User>(Messages.WrongRefreshToken);
            }
            return new ServiceResult<User>(user);
        }

        public async Task<IServiceResult> RemoveRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _unitOfWork.Repository<UserRefreshTokenDal>().GetAsync(x => x.Code == refreshToken && x.Status);
            if (existRefreshToken is null)
            {
                return new ServiceResult<User>(Messages.WrongRefreshToken);
            }
            _unitOfWork.Repository<UserRefreshTokenDal>().Delete(existRefreshToken);
            await _unitOfWork.SaveChangesAsync();
            return new ServiceResult();
        }

        public async Task<IServiceResult<RecoverPasswordView>> RecoverSavePassword(RecoverPasswordDto recoverPasswordDto)
        {
            var user = await _unitOfWork.Repository<UserDal>().GetAsync(u => u.Status && (u.Username == recoverPasswordDto.EmailOrUsername || u.Email == recoverPasswordDto.EmailOrUsername));
            if (user is null)
                return new ServiceResult<RecoverPasswordView>(Messages.UserNotFound);

            var isItDemoUser = await _unitOfWork.Repository<UserRoleDal>().IsItDemoUser(user.Id);
            if (isItDemoUser)
                return new ServiceResult<RecoverPasswordView>(Messages.DemoUserCannotChange);

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
            _unitOfWork.Repository<UserDal>().Update(user);
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
            var userRefreshToken = await _unitOfWork.Repository<UserRefreshTokenDal>().GetAsync(x => x.UserId == userId && x.Status);
            if (userRefreshToken is null)
            {
                userRefreshToken = new UserRefreshToken()
                {
                    Code = token.RefreshToken,
                    Expiration = token.RefreshTokenExpiration,
                    Status = true,
                    UserId = userId,
                };
                await _unitOfWork.Repository<UserRefreshTokenDal>().AddAsync(userRefreshToken);
            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
                userRefreshToken.Status = true;
                _unitOfWork.Repository<UserRefreshTokenDal>().Update(userRefreshToken);
            }
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
