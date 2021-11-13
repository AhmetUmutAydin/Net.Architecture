using System;
using Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Net.Architecture.Core.Utilities.IoC
{
    public static class IocManager
    {
        public static IServiceProvider _serviceProvider { get; set; }

        public static IServiceCollection Create(IServiceCollection services)
        {
            _serviceProvider = services.BuildServiceProvider();
            return services;
        }
        public static T Resolve<T>()
        {
            var accessor = _serviceProvider.GetService<IHttpContextAccessor>();
            var context = accessor.HttpContext;
            var lifetimeScope = context.RequestServices.GetService<ILifetimeScope>();
            return lifetimeScope.Resolve<T>();
        }
    }
}
