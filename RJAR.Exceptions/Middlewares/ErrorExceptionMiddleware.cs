using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RJAR.Exceptions.Constants;
using RJAR.Exceptions.Helpers;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace RJAR.Exceptions.Middlewares
{
    /// <summary>
    /// Custom middleware only defined to throw an exception if the global exception middleware
    /// is not properly configured.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ErrorExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorExceptionMiddleware> _logger;

        public ErrorExceptionMiddleware(RequestDelegate next, ILogger<ErrorExceptionMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Finalize the pipeline and generate the response with the error.
        /// </summary>
        /// <param name="context">Http context of the application.</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var errorResponseMessage = ExceptionMessageHelper.GetMiddlewareExceptionMessage();
            
            context.Response.ContentType = ExceptionConstants.CONTENT_TYPE;
            context.Response.StatusCode = errorResponseMessage.StatusCode;
            
            var exception = GetMiddlewareException(errorResponseMessage.StatusCode, errorResponseMessage.ErrorMessage);
            _logger.LogError(exception.Message, exception);

            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponseMessage));
        }

        /// <summary>
        /// Method to create a generic exception with the response message.
        /// </summary>
        /// <param name="statusCode">Response status code error generated.</param>
        /// <param name="errorDescription">Response message description for the given error.</param>
        /// <returns></returns>
        private static Exception GetMiddlewareException(Int32? statusCode, String errorDescription) => 
            new Exception($"StatusCode: {statusCode} ErrorDescription: {errorDescription}");
    }
}