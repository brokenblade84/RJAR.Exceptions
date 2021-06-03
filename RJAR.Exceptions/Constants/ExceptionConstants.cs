using System;
using System.Diagnostics.CodeAnalysis;

namespace RJAR.Exceptions.Constants
{
    [ExcludeFromCodeCoverage]
    public static class ExceptionConstants
    {
        public static readonly String CONTENT_TYPE = "application/json";
        public static readonly String EXCEPTION_DEFAULT_MESSAGE = "An error has ocurred. Please contact your system administrator for further details.";
        public static readonly String MIDDLEWARE_EXECUTION_EXCEPTION = "The global exeption middleware couldn't start because it's not properly configured.";
        
        public static readonly String MIDDLEWARE_DELEGATE_EXCEPTION = "The middleware cannot determine the next delegate to continue execution.";
    }
}
