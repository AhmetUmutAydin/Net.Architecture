using System.Threading.Tasks;
using Net.Architecture.DataAccess.Repository;
using Net.Architecture.Entities.Concrete.Auth;
using Net.Architecture.Entities.Dtos;

namespace Net.Architecture.DataAccess.Abstract.Auth
{
    public interface IUserDal : IBaseRepository<User>
    {
        Task<bool> CheckUsernameExists(string username);
        Task<bool> CheckEmailExists(string email);
        Task<bool> CheckUsernameExistsExpectOwnUser(string username,long userId);
        Task<bool> CheckEmailExistsExpectOwnUser(string email, long userId);
        Task<LayoutDto> GetUserLayoutInformation(long userId);
        Task<User> GetUserWithEmployee(string username, string moduleRole);

    }
}
