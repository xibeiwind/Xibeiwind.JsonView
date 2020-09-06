using System;
using System.Runtime.Serialization;

namespace Xibeiwind.JsonViewer
{
    [Serializable]
    public class JsonParseErrorException : Exception
    {
        public JsonParseErrorException()
        {
        }

        public JsonParseErrorException(string message) : base(message)
        {
        }

        public JsonParseErrorException(string message, Exception inner) : base(message, inner)
        {
        }

        protected JsonParseErrorException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}