using System.Collections.Generic;
using System.Threading.Tasks;
using Net.Architecture.Core.Utilities.Result;
using Net.Architecture.Entities.Dtos;

namespace Net.Architecture.Business.Abstract.Common
{
    public interface ICommunicationService
    {
        Task<IServiceResult<IEnumerable<CommunicationDto>>> GetCommunication(long id);
        Task<IServiceResult<IEnumerable<CommunicationDto>>> SaveCommunication(ContactDto contact);
    }
}
