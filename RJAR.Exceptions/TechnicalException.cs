using RJAR.Exceptions.Base;
using RJAR.Exceptions.Enumerators;
using RJAR.Exceptions.Interfaces;
using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace RJAR.Exceptions
{
    [Serializable]
    public class TechnicalException : BaseException, IBaseException
    {
        public TechnicalException()
            : base(ExceptionType.Technical)
        {

        }

        public TechnicalException(string message) 
            : base(message, ExceptionType.Technical)
        {

        }

        public TechnicalException(string message, Exception innerException) 
            : base(message, innerException, ExceptionType.Technical)
        {

        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public TechnicalException(SerializationInfo info, StreamingContext context) 
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
