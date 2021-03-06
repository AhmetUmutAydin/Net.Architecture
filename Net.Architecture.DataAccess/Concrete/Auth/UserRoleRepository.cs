using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Net.Architecture.DataAccess.Abstract.Auth;
using Net.Architecture.DataAccess.Contexts;
using Net.Architecture.DataAccess.Repository;
using Net.Architecture.Entities.Concrete.Auth;

namespace Net.Architecture.DataAccess.Concrete.Auth
{
    public class UserRoleRepository : BaseRepository<UserRole, PostgreSqlContext>, IUserRoleRepository{
        public UserRoleRepository(PostgreSqlContext PostgreSqlContext) : base(PostgreSqlContext)
        { }
        public async Task<IEnumerable<Role>> GetUserRoles(long userId)
        {
            var roles = await _context.UserRole.Include(i => i.Role).Where(r => r.Status && r.UserId == userId)
                .Select(s => s.Role).ToListAsync();
            return roles;
        }
    }
}
