using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using urlShortener.Models;

namespace urlShortener.Extensions
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
            catch(HttpRequestException  ex)
            {
                await HandleExceptionAsync(httpContext, ex, 400);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex, int? statusCode = null)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode ?? (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new ErrorDetails{
                StatusCode = statusCode ?? context.Response.StatusCode,
                Message = ex.Message
            }.ToString());
        }
        
    }
}