using RJAR.Exceptions.Interfaces;
using RJAR.Exceptions.ResponseMessages;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace RJAR.Exceptions.Tests.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class ExceptionResponseMessageHelper
    {
        public static IBaseExceptionResponseMessage GetTechnicalExceptionResponseMessage() =>
            new BaseExceptionResponseMessage
            {
                StatusCode = (Int32) HttpStatusCode.InternalServerError,
                ErrorMessage = MessageHelper.EXCEPTION_DEFAULT_MESSAGE
            };

        public static IBaseExceptionResponseMessage GetFunctionalExceptionResponseMessage() =>
            new FunctionalExceptionResponseMessage
            {
                StatusCode = (Int32) HttpStatusCode.BadRequest,
                ErrorMessage = MessageHelper.FUNCTIONAL_EXCEPTION_MESSAGE
            };
    }
}
