using Net.Architecture.Business.Abstract.Common;
using Net.Architecture.Core.Utilities.IoC;
using Net.Architecture.Entities.Enums;

namespace Net.Architecture.Business.Helpers.Concrete
{
    public static class CommunicationFactory
    {
        public static ICommunicationService CreateInstance(long deciderType)
        {
            ICommunicationService communicationService = null;
            switch (deciderType)
            {
                case (long)Enums.DeciderType.Employee:
                    communicationService = IocManager.Resolve<IEmployeeService>();
                    break;
                case (long)Enums.DeciderType.Branch:
                    communicationService = IocManager.Resolve<IBranchService>();
                    break;
                case (long)Enums.DeciderType.Member:
                    communicationService = IocManager.Resolve<IMemberService>();
                    break;
                default:
                    break;
            }
            return communicationService;
        }
    }
}
