using Microsoft.AspNetCore.Builder;
using Net.Architecture.Core.Utilities.Middlewares;

namespace Net.Architecture.Core.Extensions
{
    public static class PerformanceMiddlewareExtensions
    {
        public static void ConfigureCustomPerformanceMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<PerformanceMiddleware>();
        }
    }

}
