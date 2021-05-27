using RJAR.Exceptions.Interfaces;
using System;
using System.Diagnostics.CodeAnalysis;

namespace RJAR.Exceptions.ResponseMessages
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class BaseExceptionResponseMessage : IBaseExceptionResponseMessage
    {
        public Int32 StatusCode { get; set; }
        public String ErrorMessage { get; set; }
    }
}
