using System;
using System.Runtime.Serialization;

namespace PUN2Rx
{
    public class PUN2Exception : Exception
    {
        public short ErrorCode { get; private set; }

        public string ErrorMessage { get; private set; }

        public PUN2Exception(short errorCode, string errorMessage)
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = errorMessage;
        }

        public PUN2Exception() : base()
        {
        }

        public PUN2Exception(string message) : base(message)
        {
        }

        public PUN2Exception(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PUN2Exception(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}