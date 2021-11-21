using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Net.Architecture.DataAccess.Abstract.Auth;
using Net.Architecture.DataAccess.Contexts;
using Net.Architecture.DataAccess.Repository;
using Net.Architecture.Entities.Concrete.Auth;
using Net.Architecture.Entities.Dtos;

namespace Net.Architecture.DataAccess.Concrete.Auth
{
    public class UserRepository : BaseRepository<UserRole, PostgreSqlContext>, IUserRepository
    {
        public UserRepository(PostgreSqlContext postgreSqlContext) : base(postgreSqlContext)
        { }

        public async Task<LayoutDto> GetUserLayoutInformation(long userId)
        {
            var layout = await _context.User.Where(x => x.Status && x.Id == userId).Select(s => new LayoutDto()
            {
                Id = s.Id,
                ImageUrl = s.File != null ? s.File.Source : null,
                Username = s.Username
            }).FirstOrDefaultAsync();
            return layout;
        }
    }
}
