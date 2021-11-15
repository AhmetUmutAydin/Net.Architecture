using System.Collections.Generic;
using System.Threading.Tasks;
using Net.Architecture.DataAccess.Repository;
using Net.Architecture.Entities.Concrete.Auth;

namespace Net.Architecture.DataAccess.Abstract.Auth
{
    public interface IUserRoleDal : IBaseRepository<UserRole>
    {
        Task<IEnumerable<Role>> GetUserRoles(long userId);
        Task<bool> IsItDemoUser(long userId);
    }
}
