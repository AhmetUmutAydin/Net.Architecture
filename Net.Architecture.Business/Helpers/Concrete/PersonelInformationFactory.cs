using Net.Architecture.Business.Abstract.Common;
using Net.Architecture.Core.Utilities.IoC;
using Net.Architecture.Entities.Enums;

namespace Net.Architecture.Business.Helpers.Concrete
{
    public static class PersonalInformationFactory
    {
        public static IPersonalInformationService CreateInstance(long deciderType)
        {
            IPersonalInformationService personalInformationService = null;
            switch (deciderType)
            {
                case (long)Enums.DeciderType.Employee:
                    personalInformationService = IocManager.Resolve<IEmployeeService>();
                    break;
                case (long)Enums.DeciderType.Member:
                    personalInformationService = IocManager.Resolve<IMemberService>();
                    break;
                default:
                    break;
            }
            return personalInformationService;
        }

    }
}
