using System.Threading.Tasks;
using Net.Architecture.DataAccess.Repository;
using Net.Architecture.Entities.Concrete.Auth;

namespace Net.Architecture.DataAccess.Abstract.Auth
{
    public interface IUserRefreshTokenDal : IBaseRepository<UserRefreshToken>
    {
        Task<User> GetUserByRefreshToken(string refreshToken);
    }
}
