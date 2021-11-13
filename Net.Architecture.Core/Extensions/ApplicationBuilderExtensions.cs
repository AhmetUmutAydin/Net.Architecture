using Microsoft.Extensions.DependencyInjection;
using Net.Architecture.Core.Utilities.IoC;

namespace Net.Architecture.Core.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IServiceCollection RegisterServiceLocator(this IServiceCollection services)
        {
            return IocManager.Create(services);
        }
    }
}
