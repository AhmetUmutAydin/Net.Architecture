using Net.Architecture.DataAccess.Abstract.Common;
using Net.Architecture.DataAccess.Contexts;
using Net.Architecture.DataAccess.Repository;
using Net.Architecture.Entities.Concrete.Common;

namespace Net.Architecture.DataAccess.Concrete.Common
{
    public class DistrictDal : BaseRepository<District, PostgreSqlContext>, IDistrictDal
    {
        public DistrictDal(PostgreSqlContext PostgreSqlContext) : base(PostgreSqlContext)
        {

        }
    }
}
