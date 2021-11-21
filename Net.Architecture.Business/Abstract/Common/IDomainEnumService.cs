using System.Collections.Generic;
using System.Threading.Tasks;
using Net.Architecture.Core.Utilities.Result;
using Net.Architecture.Entities.Dtos;

namespace Net.Architecture.Business.Abstract.Common
{
    public interface IDomainEnumService
    {
        Task<IServiceResult<IEnumerable<DropdownDto>>> GetChildEnums(long parentId);
    }
}
