using Net.Architecture.Business.Abstract.Common;
using Net.Architecture.Core.Utilities.IoC;
using Net.Architecture.Entities.Enums;

namespace Net.Architecture.Business.Helpers.Concrete
{
    public static class FileFactory
    {
        public static IFileService CreateInstance(long deciderType)
        {
            IFileService fileService = null;
            switch (deciderType)
            {
                case (long)Enums.DeciderType.Employee:
                    fileService = IocManager.Resolve<IEmployeeService>();
                    break;
                case (long)Enums.DeciderType.Branch:
                    fileService = IocManager.Resolve<IBranchService>();
                    break;
                case (long)Enums.DeciderType.Member:
                    fileService = IocManager.Resolve<IMemberService>();
                    break;
                case (long)Enums.DeciderType.Institution:
                    fileService = IocManager.Resolve<IInstitutionService>();
                    break;
                default:
                    break;
            }
            return fileService;
        }
    }
}
