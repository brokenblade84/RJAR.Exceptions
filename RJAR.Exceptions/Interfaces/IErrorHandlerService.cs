using System;

namespace RJAR.Exceptions.Interfaces
{
    public interface IErrorHandlerService
    {
        IBaseResponseMessage HandleException(Exception exception);
    }
}
