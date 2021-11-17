using System.Threading.Tasks;
using Net.Architecture.Core.Utilities.Result;

namespace Net.Architecture.Business.Abstract.Common
{
    public interface IFileService
    {
        Task<IServiceResult<FileDto>> SaveFileToRoot(FileOperationsDto fileOperationsDto);
    }
}
