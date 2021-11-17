using System.Threading.Tasks;
using Net.Architecture.Core.Utilities.Result;

namespace Net.Architecture.Business.Abstract.Auth
{
    public interface IUserService
    {
        Task<IServiceResult<UserProfileDto>> GetUserProfile();
        Task<IServiceResult> SaveUserProfile(UserProfileDto userProfileDto);
        Task<IServiceResult> SaveUserPassword(UserProfileDto userProfileDto);
        Task<IServiceResult<LayoutDto>> GetUserLayout();

    }
}
