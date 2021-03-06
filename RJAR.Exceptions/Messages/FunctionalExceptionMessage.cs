using RJAR.Exceptions.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RJAR.Exceptions.Messages
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class FunctionalExceptionMessage : BaseExceptionMessage, IExtendedResponseMessage
    {
        public IDictionary<String, String> ValidationFieldMessages { get; set; }
    }
}
