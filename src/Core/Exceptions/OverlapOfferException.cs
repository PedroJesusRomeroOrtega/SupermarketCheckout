using System;
using System.Runtime.Serialization;

namespace Core.Exceptions
{
    public class OverlapOfferException : Exception
    {
        public OverlapOfferException():base("There is other offer for the same period")
        {
        }

        public OverlapOfferException(string message) : base(message)
        {
        }

        public OverlapOfferException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OverlapOfferException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
