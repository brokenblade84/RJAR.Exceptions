using RJAR.Exceptions.Interfaces;
using System;

namespace RJAR.Exceptions.ResponseMessages
{
    [Serializable]
    public class BaseExceptionResponseMessage : IBaseExceptionResponseMessage
    {
        public Int32 StatusCode { get; set; }
        public String ErrorMessage { get; set; }
    }
}
