using System.Threading.Tasks;
using Net.Architecture.Core.Utilities.Result;

namespace Net.Architecture.Business.Helpers.Abstract
{
    public interface IRoleHelper
    {
        Task<IServiceResult> SaveUserRole(long userId);
    }
}
