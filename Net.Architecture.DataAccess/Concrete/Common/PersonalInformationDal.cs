using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Net.Architecture.DataAccess.Abstract.Common;
using Net.Architecture.DataAccess.Contexts;
using Net.Architecture.DataAccess.Repository;
using Net.Architecture.Entities.Concrete.Common;
using Net.Architecture.Entities.Dtos;

namespace Net.Architecture.DataAccess.Concrete.Common
{
    public class PersonalInformationDal : BaseRepository<PersonalInformation, PostgreSqlContext>, IPersonalInformationDal
    {
        public PersonalInformationDal(PostgreSqlContext PostgreSqlContext) : base(PostgreSqlContext)
        {
        }

        public async Task<PersonalInformation> GetPersonalInformationByMemberId(long memberId, long institutionId)
        {
            var personalInformation = await _context.PersonalInformation.Where(x =>
                  x.Status && x.Member.Status && x.Member.Id == memberId && x.Member.InstitutionId == institutionId).FirstOrDefaultAsync();
            return personalInformation;
        }

        public async Task<PersonalInformation> GetPersonalInformationByEmployeeId(long employeeId, long institutionId)
        {
            var personalInformation = await _context.PersonalInformation.Where(x =>
                x.Status && x.Employee.Status && x.Employee.Id == employeeId && x.Employee.InstitutionId == institutionId).FirstOrDefaultAsync();
            return personalInformation;
        }

        public async Task<PersonalInformationDto> GetPersonalInformationDtoByEmployeeId(long employeeId, long institutionId)
        {
            var personalInformationDto = await _context.PersonalInformation.Where(x => x.Status && x.Employee.Status && x.Employee.Id == employeeId && x.Employee.InstitutionId == institutionId)
                .Select(x => new PersonalInformationDto
                {
                    Id = x.Id,
                    BirthPlace = x.BirthPlace,
                    Birthdate = x.Birthdate,
                    Gender = x.Gender,
                    ImageUrl = x.File != null ? x.File.Source : null,
                    Name = x.Name,
                    NationalIdentifier = x.NationalIdentifier,
                    Surname = x.Surname,
                    Notes = x.Notes
                }).FirstOrDefaultAsync();
            return personalInformationDto;
        }

        public async Task<PersonalInformationDto> GetPersonalInformationDtoByMemberId(long memberId, long institutionId)
        {
            var personalInformationDto = await _context.PersonalInformation.Where(x => x.Status && x.Member.Status && x.Member.Id == memberId && x.Member.InstitutionId == institutionId)
                .Select(x => new PersonalInformationDto
                {
                    Id = x.Id,
                    BirthPlace = x.BirthPlace,
                    Birthdate = x.Birthdate,
                    Gender = x.Gender,
                    ImageUrl = x.File != null ? x.File.Source : null,
                    Name = x.Name,
                    NationalIdentifier = x.NationalIdentifier,
                    Surname = x.Surname,
                    Notes = x.Notes
                }).FirstOrDefaultAsync();
            return personalInformationDto;
        }

        public async Task<IEnumerable<PersonalInformation>> GetEmployeeListWithPersonalInformation(long institutionId)
        {
            var personalList = await _context.PersonalInformation.Where(x => x.Status && (x.Member.Status && x.Member.InstitutionId == institutionId || x.Employee.Status && x.Employee.InstitutionId == institutionId)).ToListAsync();
            return personalList;
        }
    }
}
