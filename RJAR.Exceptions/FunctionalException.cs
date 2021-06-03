using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RJAR.Exceptions.Base;
using RJAR.Exceptions.Enumerators;
using RJAR.Exceptions.Helpers;
using RJAR.Exceptions.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace RJAR.Exceptions
{
    [Serializable]
    public class FunctionalException : BaseException, IFunctionalException
    {
        private IDictionary<String, String> _validationFieldMessages;

        public FunctionalException()
            : base(ExceptionType.Functional)
        {

        }

        public FunctionalException(string message, IDictionary<String, String> validationFieldMessages = null) 
            : base(message, ExceptionType.Functional)
        {
            _validationFieldMessages = validationFieldMessages ?? new Dictionary<String, String>();
        }

        public FunctionalException(string message, Exception innerException, IDictionary<String, String> validationFieldMessages = null) 
            : base(message, innerException, ExceptionType.Functional)
        {
            _validationFieldMessages = validationFieldMessages ?? new Dictionary<String, String>();
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public FunctionalException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {

        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            info.AddValue("_validationFieldMessages", _validationFieldMessages, typeof(IDictionary<String, String>));
            base.GetObjectData(info, context);
        }
        
        public IDictionary<String, String> GetValidationFieldMessages() =>
            _validationFieldMessages;

        public void SetValidationFieldMessage(IDictionary<String, String> validationFieldMessages) =>
           _validationFieldMessages = validationFieldMessages;
        
        public override void LogError()
        {
            if (_logger == null || !_logger.IsEnabled(LogLevel.Error))
                return;

            var validationFieldMessages = JsonConvert.SerializeObject(_validationFieldMessages, SerializationHelper.GetSerializationSettings());
            var message = $"Error: {this.Message}. FieldValidationMessages: {validationFieldMessages}";

            _logger.LogError(message, this);
        }
    }
}
