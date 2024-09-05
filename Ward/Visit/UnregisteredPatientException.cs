using System.Runtime.Serialization;

namespace Ward.Visit
{
    [Serializable]
    internal class UnregisteredPatientException : Exception
    {
        public UnregisteredPatientException()
        {
        }

        public UnregisteredPatientException(string? message) : base(message)
        {
        }

        public UnregisteredPatientException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnregisteredPatientException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}