using Microsoft.Extensions.Logging;
using RJAR.Exceptions.Enumerators;

namespace RJAR.Exceptions.Interfaces
{
    public interface IBaseException
    {
        ExceptionType GetExceptionType();
        void LogError();
        void SetExceptionLogger(ILogger logger);
    }
}
