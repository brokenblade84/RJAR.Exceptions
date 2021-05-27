using Microsoft.Extensions.Logging;
using RJAR.Exceptions.Helpers;
using RJAR.Exceptions.Interfaces;
using System;

namespace RJAR.Exceptions.Factory
{
    public class ErrorHandlerFactory : IErrorHandlerFactory
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<ErrorHandlerFactory> _logger;

        public ErrorHandlerFactory(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            _logger = _loggerFactory.CreateLogger<ErrorHandlerFactory>();
        }

        public IBaseExceptionResponseMessage HandleExceptionResponse(Exception exception)
        {
            _logger.LogDebug($"Starting exception handling for ex: {exception}.");

            var exceptionResponse = ExceptionMessageHelper.GetBaseExceptionMessage();

            switch (exception)
            {
                case FunctionalException fex:
                    exceptionResponse = ExceptionMessageHelper.GetFunctionalExceptionMessage(exception.Message, fex.GetValidationFieldMessages());
                    
                    fex.SetExceptionLogger(_loggerFactory?.CreateLogger<FunctionalException>());
                    fex.LogError();
                    break;
                case TechnicalException tex:
                    tex.SetExceptionLogger(_loggerFactory?.CreateLogger<TechnicalException>());
                    tex.LogError();
                    break;
                case NullReferenceException nrex:
                    var nullReferenceException = new TechnicalException(nrex.Message, nrex);

                    nullReferenceException.SetExceptionLogger(_loggerFactory?.CreateLogger<TechnicalException>());
                    nullReferenceException.LogError();
                    break;
                case ArgumentNullException nex:
                    var argumentNullException = new TechnicalException(nex.Message, nex);

                    argumentNullException.SetExceptionLogger(_loggerFactory?.CreateLogger<TechnicalException>());
                    argumentNullException.LogError();
                    break;
                case Exception ex:
                    var unhandledException = new UnhandledException(ex.Message, ex);

                    unhandledException.SetExceptionLogger(_loggerFactory?.CreateLogger<UnhandledException>());
                    unhandledException.LogError();
                    break;
            }

            _logger.LogDebug($"Finished exception handling for ex: {exception}.");

            return exceptionResponse;
        }
    }
}
