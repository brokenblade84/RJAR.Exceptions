using RJAR.Exceptions.Constants;
using RJAR.Exceptions.Interfaces;
using RJAR.Exceptions.ResponseMessages;
using System;
using System.Collections.Generic;
using System.Net;

namespace RJAR.Exceptions.Helpers
{
    public static class ExceptionMessageHelper
    {
        public static IBaseExceptionResponseMessage GetBaseExceptionMessage() =>
            new BaseExceptionResponseMessage
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                ErrorMessage = ExceptionConstants.EXCEPTION_DEFAULT_MESSAGE
            };
        public static IFunctionalExceptionResponseMessage GetFunctionalExceptionMessage(String message, IDictionary<String, String> validationFieldMessages) =>
            new FunctionalExceptionResponseMessage
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                ErrorMessage = message,
                ValidationFieldMessages = validationFieldMessages
            };
    }
}
