using RJAR.Exceptions.Constants;
using RJAR.Exceptions.Interfaces;
using RJAR.Exceptions.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace RJAR.Exceptions.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class ExceptionMessageHelper
    {
        public static IBaseExceptionMessage GetBaseExceptionMessage() =>
            new BaseExceptionMessage
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                ErrorMessage = ExceptionConstants.EXCEPTION_DEFAULT_MESSAGE
            };

        internal static IBaseExceptionMessage GetMiddlewareExceptionMessage() =>
            new BaseExceptionMessage
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                ErrorMessage = ExceptionConstants.MIDDLEWARE_EXECUTION_EXCEPTION
            };

        internal static IFunctionalExceptionMessage GetFunctionalExceptionMessage(String message, IDictionary<String, String> validationFieldMessages) =>
            new FunctionalExceptionMessage
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                ErrorMessage = message,
                ValidationFieldMessages = validationFieldMessages
            };
    }
}
