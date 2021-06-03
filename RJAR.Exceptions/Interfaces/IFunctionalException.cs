using System;
using System.Collections.Generic;

namespace RJAR.Exceptions.Interfaces
{
    public interface IFunctionalException : IBaseException
    {
        String Message { get; }
        IDictionary<String, String> GetValidationFieldMessages();
    }
}
