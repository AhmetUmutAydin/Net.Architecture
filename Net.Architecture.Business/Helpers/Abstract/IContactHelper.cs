using System.Collections.Generic;
using System.Threading.Tasks;
using Net.Architecture.Core.Utilities.Result;
using Net.Architecture.Entities.Concrete.Common;

namespace Net.Architecture.Business.Helpers.Abstract
{
    public interface IContactHelper
    {
        Task<IServiceResult<IEnumerable<CommunicationDto>>> InsertContactCommunications(IEnumerable<CommunicationDto> communicationDtos, long contactId);
        Task<IServiceResult<IEnumerable<CommunicationDto>>> SaveCommunications(IEnumerable<CommunicationDto> communicationDtos, IEnumerable<Communication> communications,long contactId);
        Task<IServiceResult<Contact>> CreateContact();
    }
}
