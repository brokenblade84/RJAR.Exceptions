using System;

namespace RJAR.Exceptions.Interfaces
{
    public interface IErrorHandlerFactory
    {
        IBaseExceptionResponseMessage HandleExceptionResponse(Exception exception);
    }
}
