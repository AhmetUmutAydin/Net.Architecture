using System.Threading.Tasks;
using Net.Architecture.Core.Utilities.Result;
using Net.Architecture.Entities.Dtos;

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
