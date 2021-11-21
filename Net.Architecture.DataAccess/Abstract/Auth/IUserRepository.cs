using System.Collections.Generic;
using System.Threading.Tasks;
using Net.Architecture.DataAccess.Repository;
using Net.Architecture.Entities.Concrete.Auth;
using Net.Architecture.Entities.Dtos;

namespace Net.Architecture.DataAccess.Abstract.Auth
{
    public interface IUserRepository : IBaseRepository<UserRole>
    {
        Task<IEnumerable<Role>> GetUserRoles(long userId);
        Task<LayoutDto> GetUserLayoutInformation(long userId);
    }
}
