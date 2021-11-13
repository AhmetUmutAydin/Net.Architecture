using Microsoft.AspNetCore.Builder;
using Net.Architecture.Core.Utilities.Middlewares;

namespace Net.Architecture.Core.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
