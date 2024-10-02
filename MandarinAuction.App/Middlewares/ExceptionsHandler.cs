using MandarinAuction.App.Exceptions.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace MandarinAuction.App.Middlewares
{
    public class ExceptionsHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionsHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UserException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(ex.Message);
            }
        }
    }

    public static class ExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseExceptionsHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionsHandler>();
        }
    }
}