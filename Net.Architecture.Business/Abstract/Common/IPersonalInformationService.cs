using System.Threading.Tasks;
using Net.Architecture.Core.Utilities.Result;
using Net.Architecture.Entities.Dtos;

namespace Net.Architecture.Business.Abstract.Common
{
    public interface IPersonalInformationService
    {
        Task<IServiceResult<PersonalInformationDto>> GetPersonalInformation(long id);
    }
}
