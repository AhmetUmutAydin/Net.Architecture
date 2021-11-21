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
            //Auth
            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<UserManager>().As<IUserService>();
            //Common
            builder.RegisterType<CityManager>().As<ICityService>();
            builder.RegisterType<DistrictManager>().As<IDistrictService>();
            builder.RegisterType<DomainEnumManager>().As<IDomainEnumService>();
            //Studio
            builder.RegisterType<UserManager>().As<IUserService>();
            //Helpers
            builder.RegisterType<TokenHelper>().As<ITokenHelper>();
            builder.RegisterType<EmailHelper>().As<IEmailHelper>();
            builder.RegisterType<ContactHelper>().As<IContactHelper>();
            builder.RegisterType<PersonalInformationHelper>().As<IPersonalInformationHelper>();
            builder.RegisterType<FileHelper>().As<IFileHelper>();
            builder.RegisterType<RoleHelper>().As<IRoleHelper>();
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>();
            #endregion
        }
    }
}

