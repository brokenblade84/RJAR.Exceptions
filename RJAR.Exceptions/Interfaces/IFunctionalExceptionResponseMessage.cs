using System;
using System.Collections.Generic;

namespace RJAR.Exceptions.Interfaces
{
    public interface IFunctionalExceptionResponseMessage : IBaseExceptionResponseMessage
    {
        IDictionary<String, String> ValidationFieldMessages { get; set; }
    }
}
