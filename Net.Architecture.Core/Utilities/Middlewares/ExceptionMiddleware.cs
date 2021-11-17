using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Net.Architecture.Core.Constants;
using Net.Architecture.Core.Utilities.Exceptions;

namespace Net.Architecture.Core.Utilities.Middlewares
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (!httpContext.Response.HasStarted)
            {
                try
                {
                    await _next(httpContext);
                }
                catch (ValidationException e)
                {
                    await ExceptionHandler.HandleExceptionAsync(httpContext, ((List<ValidationFailure>)e.Errors)[0].ErrorMessage);
                }
                catch (SpecialException e)
                {
                    await ExceptionHandler.HandleExceptionAsync(httpContext, e.Message);
                }
                catch (Exception)
                {
                    await ExceptionHandler.HandleExceptionAsync(httpContext, Messages.GlobalError);
                }
            }
        }
    }
}
