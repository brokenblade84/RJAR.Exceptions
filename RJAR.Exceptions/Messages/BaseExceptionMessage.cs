using RJAR.Exceptions.Interfaces;
using System;
using System.Diagnostics.CodeAnalysis;

namespace RJAR.Exceptions.Messages
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class BaseExceptionMessage : IBaseResponseMessage
    {
        public Int32 StatusCode { get; set; }
        public String ErrorMessage { get; set; }
    }
}
