using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Net.Architecture.Core.Utilities.Result;
using Newtonsoft.Json;

namespace Net.Architecture.Core.Utilities.Exceptions
{
    public static class ExceptionHandler
    {
        public static Task HandleExceptionAsync(HttpContext httpContext, string message)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var serviceResult = new ServiceResult(message).InternalServerError();
            return httpContext.Response.WriteAsync(
                JsonConvert.SerializeObject(new
                {
                    message = serviceResult.Message,
                    statusCode = serviceResult.StatusCode,
                    result = serviceResult.Result,
                }));
        }
    }
}
