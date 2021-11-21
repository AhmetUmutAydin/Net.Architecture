using Net.Architecture.Business.Abstract.Common;


namespace Net.Architecture.Business.Helpers.Concrete
{
    public static class CommunicationFactory
    {
        public static ICommunicationService CreateInstance(long deciderType)
        {
            ICommunicationService communicationService = null;
            //switch (deciderType)
            //{
            //    case (long)Enums.DeciderType.Employee:
            //        communicationService = IocManager.Resolve<IEmployeeService>();
            //        break;
            //    default:
            //        break;
            //}
            return communicationService;
        }
    }
}
