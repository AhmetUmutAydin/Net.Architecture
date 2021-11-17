using System.Collections.Generic;
using System.Threading.Tasks;
using Net.Architecture.Core.Utilities.Result;
using Net.Architecture.Entities.Concrete.Common;
using Net.Architecture.Entities.Dtos;

namespace Net.Architecture.Business.Helpers.Abstract
{
    public interface IPersonalInformationHelper
    {
        Task<IServiceResult<PersonalInformation>> AddPersonalInformation(PersonalInformationDto personalInformationDto);
        Task<IServiceResult> UpdatePersonalInformation(PersonalInformationDto personalInformationDto);
        Task<IServiceResult<IEnumerable<DropdownDto>>> GetPersonalInformationDropdown();
    }
}
