using Net.Architecture.DataAccess.Abstract.Common;
using Net.Architecture.DataAccess.Contexts;
using Net.Architecture.DataAccess.Repository;
using Net.Architecture.Entities.Concrete.Common;

namespace Net.Architecture.DataAccess.Concrete.Common
{
    public class AddressDal : BaseRepository<Address, PostgreSqlContext>, IAddressDal
    {
        public AddressDal(PostgreSqlContext PostgreSqlContext) : base(PostgreSqlContext)
        { }
    }
}
