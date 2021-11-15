using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Net.Architecture.DataAccess.Abstract.Common;
using Net.Architecture.DataAccess.Contexts;
using Net.Architecture.DataAccess.Repository;
using Net.Architecture.Entities.Concrete.Common;

namespace Net.Architecture.DataAccess.Concrete.Common
{
    public class AddressDal : BaseRepository<Address, PostgreSqlContext>, IAddressDal
    {
        public AddressDal(PostgreSqlContext PostgreSqlContext) : base(PostgreSqlContext)
        {

        }

        public async Task<Address> GetAddressByBranchId(long branchId, long institutionId)
        {
            var address = await _context.Address.Where(b => b.Status && b.Branch.InstitutionId == institutionId && b.Branch.Id == branchId && b.Branch.Status).FirstOrDefaultAsync();
            return address;
        }

        public async Task<Address> GetAddressByMemberId(long memberId, long institutionId)
        {
            var address = await _context.Address.Where(a => a.Status && a.PersonalInformation.Member.Id == memberId && a.PersonalInformation.Member.InstitutionId == institutionId &&
                a.PersonalInformation.Status && a.PersonalInformation.Member.Status).FirstOrDefaultAsync();
            return address;
        }

        public async Task<Address> GetAddressByEmployeeId(long employeeId, long institutionId)
        {
            var address = await _context.Address.Where(a => a.Status && a.PersonalInformation.Employee.Id == employeeId && a.PersonalInformation.Employee.InstitutionId == institutionId &&
                                                            a.PersonalInformation.Status && a.PersonalInformation.Employee.Status).FirstOrDefaultAsync();
            return address;
        }
    }
}
