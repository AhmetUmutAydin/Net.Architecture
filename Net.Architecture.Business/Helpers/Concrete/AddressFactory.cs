using Net.Architecture.Business.Abstract.Common;

namespace Net.Architecture.Business.Helpers.Concrete
{
    public static class AddressFactory
    {
        public static IAddressService CreateInstance(long deciderType)
        {
            IAddressService addressService = null;
            //switch (deciderType)
            //{
            //    case (long)Enums.DeciderType.Employee:
            //        addressService = IocManager.Resolve<IEmployeeService>();
            //        break;
            //    default:
            //        break;
            //}
            return addressService;
        }
    }
}
