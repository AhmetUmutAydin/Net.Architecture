using Net.Architecture.Business.Abstract.Common;
using Net.Architecture.Core.Utilities.IoC;
using Net.Architecture.Entities.Enums;

namespace Net.Architecture.Business.Helpers.Concrete
{
    public static class AddressFactory
    {
        public static IAddressService CreateInstance(long deciderType)
        {
            IAddressService addressService = null;
            switch (deciderType)
            {
                case (long)Enums.DeciderType.Employee:
                    addressService = IocManager.Resolve<IEmployeeService>();
                    break;
                case (long)Enums.DeciderType.Branch:
                    addressService = IocManager.Resolve<IBranchService>();
                    break;
                case (long)Enums.DeciderType.Member:
                    addressService = IocManager.Resolve<IMemberService>();
                    break;
                default:
                    break;
            }
            return addressService;
        }
    }
}
