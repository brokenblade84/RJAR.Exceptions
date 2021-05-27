using System;

namespace RJAR.Exceptions.Interfaces
{
    public interface IBaseExceptionResponseMessage
    {
        Int32 StatusCode { get; set; }
        String ErrorMessage { get; set; }
    }
}
