using Microsoft.Extensions.Logging;
using RJAR.Exceptions.Base;
using RJAR.Exceptions.Helpers;
using RJAR.Exceptions.Interfaces;
using System;
using System.IO;

namespace RJAR.Exceptions.Factories
{
    /// <summary>
    /// Class to handle the different types of errors produced in the application.
    /// </summary>
    public class ErrorHandlerFactory : IErrorHandlerFactory
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<BaseException> _logger;

        public ErrorHandlerFactory(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            _logger = loggerFactory?.CreateLogger<BaseException>() ?? throw new ArgumentNullException(nameof(_logger));
        }

        public IBaseExceptionMessage HandleExceptionResponse(Exception exception)
        {
            IBaseException customException = null;

            var exceptionResponse = ExceptionMessageHelper.GetBaseExceptionMessage();

            switch (exception)
            {
                case FunctionalException fex:
                    exceptionResponse = ExceptionMessageHelper.GetFunctionalExceptionMessage(exception.Message, fex.GetValidationFieldMessages());
                    customException = fex;
                    break;
                case TechnicalException tex:
                    customException = tex;
                    break;
                case NullReferenceException nrex:
                    customException = new TechnicalException(nrex.Message, nrex);
                    break;
                case ArgumentNullException nex:
                    customException = new TechnicalException(nex.Message, nex);
                    break;
                case IndexOutOfRangeException indexOutOfRangeEx:
                    customException = new TechnicalException(indexOutOfRangeEx.Message, indexOutOfRangeEx);
                    break;
                case IOException ioEx:
                    customException = new TechnicalException(ioEx.Message, ioEx);
                    break;
                case Exception ex:
                    customException = new UnhandledException(ex.Message, ex);
                    break;
            }

            customException.SetExceptionLogger(_logger);
            customException.LogError();

            return exceptionResponse;
        }
    }
}
