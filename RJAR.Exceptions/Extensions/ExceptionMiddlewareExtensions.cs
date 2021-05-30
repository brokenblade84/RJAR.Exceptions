using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RJAR.Exceptions.Factories;
using RJAR.Exceptions.Interfaces;
using RJAR.Exceptions.Middlewares;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace RJAR.Exceptions.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// Method to set up the services required for the global exception middleware.
        /// A single call to this method must be done.
        /// </summary>
        /// <param name="services">Collection of services defined in the application.</param>
        /// <returns>Updated collection of services for the application.</returns>
        public static IServiceCollection UseExceptionMiddleware(this IServiceCollection services)
        {
            services.RemoveServiceFromCollection<IErrorHandlerFactory>()
                    .AddTransient<IErrorHandlerFactory, ErrorHandlerFactory>();
            
            return services;
        }

        /// <summary>
        /// Extension method to implement global exception handling.
        /// </summary>
        /// <param name="app"></param>
        public static void UseExceptionMiddleware(this IApplicationBuilder app)
        {
            //Error handle factory service must be defined. If not a call to UseExceptionMiddleware was omitted.
            var errorHandlerFactory = app.ApplicationServices.GetService<IErrorHandlerFactory>();
     
            if (errorHandlerFactory == null)
                app.UseMiddleware<ErrorExceptionMiddleware>();
            else
                app.UseMiddleware<ExceptionMiddleware>();
        }

        /// <summary>
        /// Internal method to remove a specific service from the collection. (if exists)
        /// </summary>
        /// <typeparam name="T">Type of the service to remove.</typeparam>
        /// <param name="services">Collection of services defined in the application.</param>
        /// <returns>Updated collection of services for the application.</returns>
        internal static IServiceCollection RemoveServiceFromCollection<T>(this IServiceCollection services)
        {
            if (services.Any(x => x.ServiceType == typeof(T)))
            {
                var serviceDescriptors = services.Where(descriptor => descriptor.ServiceType == typeof(T)).ToList();

                foreach (var serviceDescriptor in serviceDescriptors)
                    services.Remove(serviceDescriptor);
            }

            return services;
        }
    }
}