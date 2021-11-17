using System.Collections.Generic;
using System.Threading.Tasks;
using Net.Architecture.Core.Utilities.Result;

namespace Net.Architecture.Business.Abstract.Common
{
    public interface ICityService
    {
        Task<IServiceResult<IEnumerable<DropdownDto>>> GetCities();
    }
}
