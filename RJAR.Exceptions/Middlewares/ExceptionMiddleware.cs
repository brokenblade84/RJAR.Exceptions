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
        private readonly IErrorHandlerService _errorHandlerService;

        public ExceptionMiddleware(RequestDelegate next, IErrorHandlerService errorHandlerService, ILogger<ExceptionMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _errorHandlerService = errorHandlerService ?? throw new ArgumentNullException(nameof(errorHandlerService));
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
            var errorResponse = _errorHandlerService.HandleException(exception);
            
            context.Response.ContentType = ExceptionConstants.CONTENT_TYPE;
            context.Response.StatusCode = errorResponse.StatusCode;
            
            return context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }
    }
}