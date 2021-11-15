using System.Threading.Tasks;
using Net.Architecture.DataAccess.Repository;
using Net.Architecture.Entities.Concrete.Common;

namespace Net.Architecture.DataAccess.Abstract.Common
{
    public interface IAddressDal : IBaseRepository<Address>
    {
        Task<Address> GetAddressByBranchId(long branchId, long institutionId);
        Task<Address> GetAddressByMemberId(long memberId, long institutionId);
        Task<Address> GetAddressByEmployeeId(long employeeId, long institutionId);

    }
}
