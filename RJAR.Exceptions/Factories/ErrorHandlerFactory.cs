using Microsoft.Extensions.Logging;
using RJAR.Exceptions.Base;
using RJAR.Exceptions.Helpers;
using RJAR.Exceptions.Interfaces;
using System;
using System.Data.SqlClient;
using System.IO;

namespace RJAR.Exceptions.Factories
{
    /// <summary>
    /// Class to handle the different types of errors produced in the application.
    /// </summary>
    public class ErrorHandlerFactory : IErrorHandlerFactory
    {
        private readonly ILoggerFactory _loggerFactory;

        public ErrorHandlerFactory(ILoggerFactory loggerFactory) =>
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));

        public IBaseExceptionMessage HandleExceptionResponse(Exception exception)
        {
            IBaseException customException = null;
            ILogger<IBaseException> exceptionLogger = _loggerFactory?.CreateLogger<BaseException>();

            var exceptionResponse = ExceptionMessageHelper.GetBaseExceptionMessage();

            switch (exception)
            {
                case FunctionalException fex:
                    exceptionResponse = ExceptionMessageHelper.GetFunctionalExceptionMessage(exception.Message, fex.GetValidationFieldMessages());
                    break;
                case TechnicalException tex:
                    exceptionLogger = _loggerFactory?.CreateLogger<TechnicalException>();
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
                case SqlException sqlEx:
                    customException = new TechnicalException(sqlEx.Message, sqlEx);
                    break;
                case Exception ex:
                    customException = new UnhandledException(ex.Message, ex);
                    break;
            }

            customException.SetExceptionLogger(exceptionLogger);
            customException.LogError();

            return exceptionResponse;
        }
    }
}
