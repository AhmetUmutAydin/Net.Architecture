using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Net.Architecture.DataAccess.Abstract.Common;
using Net.Architecture.DataAccess.Contexts;
using Net.Architecture.DataAccess.Repository;
using Net.Architecture.Entities.Concrete.Common;

namespace Net.Architecture.DataAccess.Concrete.Common
{
    public class CommunicationDal : BaseRepository<Communication, PostgreSqlContext>, ICommunicationDal
    {
        public CommunicationDal(PostgreSqlContext PostgreSqlContext) : base(PostgreSqlContext)
        {

        }

        public async Task<IEnumerable<Communication>> GetCommunicationsByBranchId(long branchId, long institutionId)
        {
            var communications = await _context.Communication.Where(c => c.Status && c.Contact.Branch.InstitutionId == institutionId && c.Contact.Branch.Id == branchId
                                                                         && c.Contact.Status &&c.Contact.Branch.Status).ToListAsync();
            return communications;
        }

        public async Task<IEnumerable<Communication>> GetCommunicationsByMemberId(long memberId, long institutionId)
        {
            var communications = await _context.Communication.Where(c =>
                c.Status && c.Contact.PersonalInformation.Member.InstitutionId == institutionId && c.Contact.PersonalInformation.Member.Id == memberId
                && c.Contact.PersonalInformation.Member.Status && c.Contact.PersonalInformation.Status && c.Contact.Status).ToListAsync();
            return communications;
        }

        public async Task<IEnumerable<Communication>> GetCommunicationsByEmployeeId(long employeeId, long institutionId)
        {
            var communications = await _context.Communication.Where(c =>
                c.Status && c.Contact.PersonalInformation.Employee.InstitutionId == institutionId && c.Contact.PersonalInformation.Employee.Id == employeeId
                && c.Contact.PersonalInformation.Employee.Status && c.Contact.PersonalInformation.Status && c.Contact.Status).ToListAsync();
            return communications;
        }
    }
}
