using System.Collections.Generic;
using System.Threading.Tasks;
using Net.Architecture.DataAccess.Repository;
using Net.Architecture.Entities.Concrete.Common;

namespace Net.Architecture.DataAccess.Abstract.Common
{
    public interface ICommunicationDal : IBaseRepository<Communication>
    {
        Task<IEnumerable<Communication>> GetCommunicationsByBranchId(long branchId, long institutionId);
        Task<IEnumerable<Communication>> GetCommunicationsByMemberId(long memberId, long institutionId);
        Task<IEnumerable<Communication>> GetCommunicationsByEmployeeId(long employeeId, long institutionId);
    }
}
