using Net.Architecture.DataAccess.Abstract.Auth;
using Net.Architecture.DataAccess.Contexts;
using Net.Architecture.DataAccess.Repository;
using Net.Architecture.Entities.Concrete.Auth;

namespace Net.Architecture.DataAccess.Concrete.Auth
{
    public class RoleDal : BaseRepository<Role, PostgreSqlContext>, IRoleDal
    {
        public RoleDal(PostgreSqlContext PostgreSqlContext) : base(PostgreSqlContext)
        {}
    }
}
