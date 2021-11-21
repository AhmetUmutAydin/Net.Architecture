using System.Threading.Tasks;
using Net.Architecture.Business.Abstract;
using Net.Architecture.Business.Abstract.Auth;
using Net.Architecture.Business.Helpers.Abstract;
using Net.Architecture.Core.Constants;
using Net.Architecture.Core.Utilities.Result;
using Net.Architecture.Core.Utilities.Security.Hashing;
using Net.Architecture.DataAccess.UnitOfWork;
using Net.Architecture.Entities.Dtos;

namespace Net.Architecture.Business.Concrete.Auth
{
    public class UserManager : BaseBusiness, IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileHelper _fileHelper;

        public UserManager(IUnitOfWork unitOfWork, IFileHelper fileHelper)
        {
            _unitOfWork = unitOfWork;
            _fileHelper = fileHelper;
        }

        public async Task<IServiceResult<UserProfileDto>> GetUserProfile()
        {
            var userProfile = await _unitOfWork.Repository<UserDal>().GetAsync(x => x.Status && x.Id == JwtUserId);
            var result = userProfile.ToDto<UserProfileDto>();
            return new ServiceResult<UserProfileDto>(result);
        }

        public async Task<IServiceResult> SaveUserProfile(UserProfileDto userProfileDto)
        {
            var userProfile = await _unitOfWork.Repository<UserDal>().GetAsync(x => x.Status && x.Id == JwtUserId);
            var isEmailExists = await _unitOfWork.Repository<UserDal>().CheckEmailExistsExpectOwnUser(userProfileDto.Email, JwtUserId);
            if (isEmailExists)
            {
                return new ServiceResult(Messages.EmailExists, false);
            }

            var isUserExists = await _unitOfWork.Repository<UserDal>().CheckUsernameExistsExpectOwnUser(userProfileDto.Username, JwtUserId);
            if (isUserExists)
            {
                return new ServiceResult(Messages.UsernameExists, false);
            }

            userProfile.Email = userProfileDto.Email;
            userProfile.Username = userProfileDto.Username;

             _unitOfWork.Repository<UserDal>().Update(userProfile);
            await _unitOfWork.SaveChangesAsync();

            return new ServiceResult();
        }

        public async Task<IServiceResult> SaveUserPassword(UserProfileDto userProfileDto)
        {
            var user = await _unitOfWork.Repository<UserDal>().GetAsync(x => x.Status && x.Id == JwtUserId);
            if (!HashingHelper.VerifyPasswordHash(userProfileDto.OldPassword, user.PasswordHash, user.PasswordSalt))
            {
                return new ServiceResult(Messages.WrongPassword);
            }

            byte[] passwordHash;
            byte[] passwordSalt;
            HashingHelper.CreatePasswordHash(userProfileDto.NewPassword, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _unitOfWork.Repository<UserDal>().Update(user);
            await _unitOfWork.SaveChangesAsync();

            return new ServiceResult();
        }

        public async Task<IServiceResult<LayoutDto>> GetUserLayout()
        {
            var layout = await _unitOfWork.Repository<UserDal>().GetUserLayoutInformation(JwtUserId);
            if (layout.ImageUrl != null)
                layout.ImageUrl = _fileHelper.GetRootUrlWithSource(layout.ImageUrl);
            return new ServiceResult<LayoutDto>(layout);
        }
    }
}
