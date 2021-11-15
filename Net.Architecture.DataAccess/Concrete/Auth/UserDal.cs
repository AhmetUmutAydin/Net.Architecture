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
    public class UserDal : BaseRepository<User, PostgreSqlContext>, IUserDal
    {
        public UserDal(PostgreSqlContext PostgreSqlContext) : base(PostgreSqlContext)
        {
        }

        public async Task<bool> CheckEmailExists(string email)
        {
            var isItExists = await _context.User.AnyAsync(u => u.Email == email);
            return isItExists;
        }

        public async Task<bool> CheckUsernameExists(string username)
        {
            var isItExists = await _context.User.AnyAsync(u => u.Username == username);
            return isItExists;
        }

        public async Task<bool> CheckEmailExistsExpectOwnUser(string email, long userId)
        {
            var isItExists = await _context.User.AnyAsync(u => u.Email == email && u.Status && u.Id != userId);
            return isItExists;
        }

        public async Task<bool> CheckUsernameExistsExpectOwnUser(string username, long userId)
        {
            var isItExists = await _context.User.AnyAsync(u => u.Username == username && u.Status && u.Id != userId);
            return isItExists;
        }

        public async Task<LayoutDto> GetUserLayoutInformation(long userId)
        {
            var layout = await _context.User.Where(x => x.Status && x.Id == userId).Select(s => new LayoutDto()
            {
                Id = s.Id,
                ImageUrl = s.Employee.PersonalInformation.File != null ? s.Employee.PersonalInformation.File.Source : null,
                InstitutionName = s.Employee.Institution.Name,
                Username = s.Username
            }).FirstOrDefaultAsync();
            return layout;
        }

        public async Task<User> GetUserWithEmployee(string username, string moduleRole)
        {
            var user = await _context.User.Include(i => i.Employee).FirstOrDefaultAsync(u => (u.Username == username || u.Email == username) && u.UserRole.Any(a => (a.Role.Name == moduleRole || (moduleRole == Constants.CompanyRoleName && a.Role.Name == Constants.DemoRoleName)) && a.Status) && u.Status && u.Employee.Status);
            return user;
        }
    }
}
