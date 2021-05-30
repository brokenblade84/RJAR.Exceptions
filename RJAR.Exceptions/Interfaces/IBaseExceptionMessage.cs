using System;

namespace RJAR.Exceptions.Interfaces
{
    public interface IBaseExceptionMessage
    {
        Int32 StatusCode { get; set; }
        String ErrorMessage { get; set; }
    }
}
