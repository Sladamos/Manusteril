using System.Runtime.Serialization;

namespace Emergency.Patient
{
    [Serializable]
    internal class InvalidPatientDataException : Exception
    {
        public InvalidPatientDataException()
        {
        }

        public InvalidPatientDataException(string? message) : base(message)
        {
        }

        public InvalidPatientDataException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidPatientDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}