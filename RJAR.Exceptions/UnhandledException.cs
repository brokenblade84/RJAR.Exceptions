using RJAR.Exceptions.Base;
using RJAR.Exceptions.Enumerators;
using RJAR.Exceptions.Interfaces;
using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace RJAR.Exceptions
{
    [Serializable]
    internal class UnhandledException : BaseException, IBaseException
    {
        public UnhandledException() 
            : base(ExceptionType.Unhandled)
        {

        }

        public UnhandledException(string message) 
            : base(message, ExceptionType.Unhandled)
        {

        }

        public UnhandledException(string message, Exception innerException) 
            : base(message, innerException, ExceptionType.Unhandled)
        {

        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public UnhandledException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {

        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            base.GetObjectData(info, context);
        }
    }
}
