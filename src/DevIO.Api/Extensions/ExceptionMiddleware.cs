using Microsoft.AspNetCore.Http;
using System;
using Elmah.Io.AspNetCore;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace DevIO.Api.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                ex.Ship(httpContext);
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }

    }
    public static class ErrorLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
