using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RJAR.Exceptions.Constants;
using RJAR.Exceptions.Interfaces;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace RJAR.Exceptions.Middlewares
{
    [ExcludeFromCodeCoverage]
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IErrorHandlerFactory _errorHandlerFactory;

        public ExceptionMiddleware(RequestDelegate next, IErrorHandlerFactory errorHandlerFactory, ILogger<ExceptionMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _errorHandlerFactory = errorHandlerFactory ?? throw new ArgumentNullException(nameof(errorHandlerFactory));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                _logger.LogDebug("Initialize custom global error handler.");
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex).ConfigureAwait(false);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorResponse = _errorHandlerFactory.HandleExceptionResponse(exception);
            
            context.Response.ContentType = ExceptionConstants.CONTENT_TYPE;
            context.Response.StatusCode = errorResponse.StatusCode;
            
            return context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }
    }
}