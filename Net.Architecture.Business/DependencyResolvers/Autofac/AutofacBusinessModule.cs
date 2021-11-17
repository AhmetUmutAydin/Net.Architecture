using Autofac;
using Net.Architecture.Business.Abstract.Auth;
using Net.Architecture.Business.Abstract.Common;
using Net.Architecture.Business.Concrete.Auth;
using Net.Architecture.Business.Concrete.Common;
using Net.Architecture.Business.Helpers.Abstract;
using Net.Architecture.Business.Helpers.Concrete;
using Net.Architecture.Core.CrossCuttingConcerns.Caching;
using Net.Architecture.DataAccess.UnitOfWork;

namespace Net.Architecture.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            #region Business injections
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            //Admin
            builder.RegisterType<AdminManager>().As<IAdminService>();
            //Auth
            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<UserManager>().As<IUserService>();
            //Common
            builder.RegisterType<CityManager>().As<ICityService>();
            builder.RegisterType<DistrictManager>().As<IDistrictService>();
            builder.RegisterType<DomainEnumManager>().As<IDomainEnumService>();
            builder.RegisterType<RegisterManager>().As<IRegisterService>();
            //Studio
            builder.RegisterType<BranchManager>().As<IBranchService>();
            builder.RegisterType<MemberManager>().As<IMemberService>();
            builder.RegisterType<EmployeeManager>().As<IEmployeeService>();
            builder.RegisterType<InstitutionManager>().As<IInstitutionService>();
            builder.RegisterType<InvitationManager>().As<IInvitationService>();
            builder.RegisterType<MemberManager>().As<IMemberService>();
            builder.RegisterType<TrainingTypeManager>().As<ITrainingTypeService>();
            builder.RegisterType<EmployeeWageManager>().As<IEmployeeWageService>();
            builder.RegisterType<MemberFeeManager>().As<IMemberFeeService>();
            builder.RegisterType<TrainingManager>().As<ITrainingService>();
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<StatisticsManager>().As<IStatisticsService>();
            builder.RegisterType<MembershipManager>().As<IMembershipService>();
            //Helpers
            builder.RegisterType<TokenHelper>().As<ITokenHelper>();
            builder.RegisterType<EmailHelper>().As<IEmailHelper>();
            builder.RegisterType<ContactHelper>().As<IContactHelper>();
            builder.RegisterType<PersonalInformationHelper>().As<IPersonalInformationHelper>();
            builder.RegisterType<TrainingTypeHelper>().As<ITrainingTypeHelper>();
            builder.RegisterType<FileHelper>().As<IFileHelper>();
            builder.RegisterType<RoleHelper>().As<IRoleHelper>();
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>();
            #endregion

            #region Interceptor Configuration //Aspect yapısına ihtiyaç kalmadığından dolayı kullanılmıyor ilerde yorum kaldırılarak kullanılabilir

            //var assembly = GetType().Assembly;
            //builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(new ProxyGenerationOptions()
            //{
            //    Selector = new AspectInterceptorSelector()
            //}).SingleInstance();

            #endregion

        }
    }
}
//örnek property injection
//builder.RegisterType<TestManager>().As<ITestService>().OnActivated(a =>
//{
//    a.Context.
//});

// builder.Register(c => new TestManager(){UnitOfWork = c.Resolve<IUnitOfWork>()});//Property injection örneği
