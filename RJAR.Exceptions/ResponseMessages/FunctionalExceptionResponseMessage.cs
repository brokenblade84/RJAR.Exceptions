using RJAR.Exceptions.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RJAR.Exceptions.ResponseMessages
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class FunctionalExceptionResponseMessage : BaseExceptionResponseMessage, IFunctionalExceptionResponseMessage
    {
        public IDictionary<String, String> ValidationFieldMessages { get; set; }
    }
}
