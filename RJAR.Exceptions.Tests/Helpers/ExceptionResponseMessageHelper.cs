using RJAR.Exceptions.Interfaces;
using RJAR.Exceptions.Messages;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace RJAR.Exceptions.Tests.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class ExceptionResponseMessageHelper
    {
        public static IBaseExceptionMessage GetTechnicalExceptionResponseMessage() =>
            new BaseExceptionMessage
            {
                StatusCode = (Int32) HttpStatusCode.InternalServerError,
                ErrorMessage = MessageHelper.EXCEPTION_DEFAULT_MESSAGE
            };

        public static IBaseExceptionMessage GetFunctionalExceptionResponseMessage() =>
            new FunctionalExceptionMessage
            {
                StatusCode = (Int32) HttpStatusCode.BadRequest,
                ErrorMessage = MessageHelper.FUNCTIONAL_EXCEPTION_MESSAGE
            };
    }
}
