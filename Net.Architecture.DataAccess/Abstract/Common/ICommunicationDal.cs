using System.Collections.Generic;
using System.Threading.Tasks;
using Net.Architecture.DataAccess.Repository;
using Net.Architecture.Entities.Concrete.Common;

namespace Net.Architecture.DataAccess.Abstract.Common
{
    public interface ICommunicationDal : IBaseRepository<Communication>
    {
    }
}
