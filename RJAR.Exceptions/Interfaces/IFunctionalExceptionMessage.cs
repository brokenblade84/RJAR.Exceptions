using System;
using System.Collections.Generic;

namespace RJAR.Exceptions.Interfaces
{
    public interface IFunctionalExceptionMessage : IBaseExceptionMessage
    {
        IDictionary<String, String> ValidationFieldMessages { get; set; }
    }
}
