using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RJAR.Exceptions.Factory;
using RJAR.Exceptions.Interfaces;
using RJAR.Exceptions.Middlewares;

namespace RJAR.Exceptions.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static IServiceCollection UseExceptionMiddleware(this IServiceCollection services)
        {
            services.AddTransient<IErrorHandlerFactory, ErrorHandlerFactory>();
            return services;
        }

        public static void UseExceptionMiddleware(this IApplicationBuilder app) =>
            app.UseMiddleware<ExceptionMiddleware>();
    }
}