using Microsoft.Extensions.Logging;
using RJAR.Exceptions.Enumerators;
using RJAR.Exceptions.Interfaces;
using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace RJAR.Exceptions.Base
{
    [Serializable]
    public abstract class BaseException : Exception, IBaseException
    {
        private readonly ExceptionType _exceptionType;
        protected ILogger<BaseException> _logger;
        
        protected BaseException(ExceptionType exceptionType = ExceptionType.Unhandled) 
            : base()
        {
            _exceptionType = exceptionType;
        }

        protected BaseException(string message, ExceptionType exceptionType = ExceptionType.Unhandled) 
            : base(message)
        {
            _exceptionType = exceptionType;
        }

        protected BaseException(string message, Exception innerException, ExceptionType exceptionType = ExceptionType.Unhandled) 
            : base(message, innerException)
        {
            _exceptionType = exceptionType;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected BaseException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
            _exceptionType = (ExceptionType)info.GetValue("_exceptionType", typeof(ExceptionType));
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            info.AddValue("_exceptionType", _exceptionType, typeof(ExceptionType));
            base.GetObjectData(info, context);
        }

        public ExceptionType GetExceptionType() => _exceptionType;

        public virtual void LogError()
        {
            if (_logger != null && _logger.IsEnabled(LogLevel.Error))
            {
                _logger.LogError(this.Message, this);
            }
        }

        public void SetExceptionLogger<T>(ILogger<T> logger) where T : IBaseException =>
            _logger = (ILogger<BaseException>) logger;
    }
}
