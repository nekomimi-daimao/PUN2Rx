using System;
using System.Runtime.Serialization;

namespace PUN2Rx
{
    public class PUN2Exception : Exception
    {
        public static PUN2Exception Create(short errorCode, string message)
        {
            return new PUN2Exception(message) {ErrorCode = errorCode};
        }

        public short ErrorCode { get; private set; }

        public override string ToString()
        {
            return $"{base.ToString()}, {nameof(ErrorCode)}: {ErrorCode}";
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