using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Net.Architecture.DataAccess.Abstract.Auth;
using Net.Architecture.DataAccess.Contexts;
using Net.Architecture.DataAccess.Repository;
using Net.Architecture.Entities.Concrete.Auth;

namespace Net.Architecture.DataAccess.Concrete.Auth
{
    public class UserRefreshTokenDal : BaseRepository<UserRefreshToken, PostgreSqlContext>, IUserRefreshTokenDal
    {
        public UserRefreshTokenDal(PostgreSqlContext PostgreSqlContext) : base(PostgreSqlContext)
        { }

        public async Task<User> GetUserByRefreshToken(string refreshToken)
        {
            var userRefreshToken = await _context.UserRefreshToken.Include(i => i.User.Employee).Where(x => x.Code == refreshToken && x.Status && x.User.Employee.Status && x.User.Status).SingleOrDefaultAsync();
            return userRefreshToken?.User;
        }
    }
}
