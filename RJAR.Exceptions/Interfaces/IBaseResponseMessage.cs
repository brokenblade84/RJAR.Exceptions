using System;

namespace RJAR.Exceptions.Interfaces
{
    public interface IBaseResponseMessage
    {
        Int32 StatusCode { get; set; }
        String ErrorMessage { get; set; }
    }
}
