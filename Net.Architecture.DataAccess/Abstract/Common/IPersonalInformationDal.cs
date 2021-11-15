using System.Collections.Generic;
using System.Threading.Tasks;
using Net.Architecture.DataAccess.Repository;
using Net.Architecture.Entities.Concrete.Common;
using Net.Architecture.Entities.Dtos;

namespace Net.Architecture.DataAccess.Abstract.Common
{
    public interface IPersonalInformationDal : IBaseRepository<PersonalInformation>
    {
        Task<PersonalInformation> GetPersonalInformationByMemberId(long memberId, long institutionId);
        Task<PersonalInformation> GetPersonalInformationByEmployeeId(long employeeId, long institutionId);
        Task<PersonalInformationDto> GetPersonalInformationDtoByEmployeeId(long employeeId, long institutionId);
        Task<PersonalInformationDto> GetPersonalInformationDtoByMemberId(long memberId, long institutionId);
        Task<IEnumerable<PersonalInformation>> GetEmployeeListWithPersonalInformation(long institutionId);
    }
}
