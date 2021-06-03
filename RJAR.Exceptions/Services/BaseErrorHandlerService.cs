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
    public class BaseErrorHandlerService : IErrorHandlerService
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<BaseException> _logger;

        public BaseErrorHandlerService(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            _logger = _loggerFactory?.CreateLogger<BaseException>() ?? throw new ArgumentNullException(nameof(_logger));
        }

        /// <summary>
        /// Method to handle the exception and generate the appropiate response message.
        /// </summary>
        /// <param name="exception">Exception raised in the application.</param>
        /// <returns>Request response with custom message.</returns>
        public IBaseResponseMessage HandleException(Exception exception)
        {
            IBaseException customException = GetException(exception);
            var exceptionResponse = ExceptionMessageHelper.GetBaseExceptionMessage();

            if (customException is IFunctionalException)
            {
                var funEx = customException as IFunctionalException;
                exceptionResponse = ExceptionMessageHelper.GetFunctionalExceptionMessage(funEx.Message, funEx.GetValidationFieldMessages());
            }

            customException.SetExceptionLogger(_logger);
            customException.LogError();

            return exceptionResponse;
        }

        /// <summary>
        /// Virtual method to extend the base service error handling.
        /// </summary>
        /// <param name="exception">Exception raised in the application.</param>
        /// <returns>Custom Exception created.</returns>
        protected virtual IBaseException GetException(Exception exception) =>
            exception switch
            {
                FunctionalException functionalException => functionalException,
                TechnicalException technicalException => technicalException,
                NullReferenceException nullReferenceException => new TechnicalException(nullReferenceException.Message, nullReferenceException),
                ArgumentNullException argumentNullException => new TechnicalException(argumentNullException.Message, argumentNullException),
                IndexOutOfRangeException indexOutOfRangeEx => new TechnicalException(indexOutOfRangeEx.Message, indexOutOfRangeEx),
                IOException ioEx => new TechnicalException(ioEx.Message, ioEx),
                _ => new UnhandledException(exception.Message, exception),
            };
    }
}
