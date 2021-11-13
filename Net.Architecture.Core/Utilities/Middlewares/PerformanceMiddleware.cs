using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Net.Architecture.Core.Utilities.Middlewares
{
    public class PerformanceMiddleware
    {
        private RequestDelegate _next;

        public PerformanceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var correlationId = httpContext.TraceIdentifier;

            var stopwatch = Stopwatch.StartNew();
            httpContext.Response.OnStarting(() =>
            {
                httpContext.Response.Headers["X-Request-Duration"] = stopwatch.Elapsed.TotalMilliseconds.ToString();
                return Task.CompletedTask;
            });

            await _next(httpContext);
            var duration = stopwatch.Elapsed.TotalMilliseconds;
            //we can set critical point for duration and whenever it passes the point we can send email or log  the request 
        }
    }
}
