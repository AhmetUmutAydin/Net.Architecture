using System.Threading.Tasks;
using Net.Architecture.Core.Utilities.Result;
using Net.Architecture.Entities.Concrete.Common;
using Net.Architecture.Entities.Dtos;

namespace Net.Architecture.Business.Helpers.Abstract
{
    public interface IFileHelper
    {
        Task<ServiceResult<File>> SaveFileToRoot(FileOperationsDto fileOperationsDto);
        string GetRootUrlWithSource(string source);
        Task<ServiceResult<long>> GetFileIdByRootİmageUrl(string imageUrl);

    }
}
