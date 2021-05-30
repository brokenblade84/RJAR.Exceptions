using System;

namespace RJAR.Exceptions.Interfaces
{
    public interface IErrorHandlerFactory
    {
        IBaseExceptionMessage HandleExceptionResponse(Exception exception);
    }
}
