using RJAR.Exceptions.Interfaces;
using System;
using System.Collections.Generic;

namespace RJAR.Exceptions.ResponseMessages
{
    [Serializable]
    public class FunctionalExceptionResponseMessage : BaseExceptionResponseMessage, IFunctionalExceptionResponseMessage
    {
        public IDictionary<String, String> ValidationFieldMessages { get; set; }
    }
}
