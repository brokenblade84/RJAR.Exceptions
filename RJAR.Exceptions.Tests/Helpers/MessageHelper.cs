using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RJAR.Exceptions.Tests.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class MessageHelper
    {
        public static String FUNCTIONAL_EXCEPTION_MESSAGE = "This is a functional exception message";
        public static String TECHNICAL_EXCEPTION_MESSAGE = "This is a technical exception message";
        public static String EXCEPTION_DEFAULT_MESSAGE = "An error has ocurred. Please contact your system administrator for further details.";

        public static IDictionary<String, String> ValidateFieldMessages = new Dictionary<String, String>() { 
        
        };
    }
}
