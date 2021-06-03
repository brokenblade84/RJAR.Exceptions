using System;
using System.Collections.Generic;

namespace RJAR.Exceptions.Interfaces
{
    public interface IExtendedResponseMessage : IBaseResponseMessage
    {
        IDictionary<String, String> ValidationFieldMessages { get; set; }
    }
}
