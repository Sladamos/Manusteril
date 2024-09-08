using System.Runtime.Serialization;

namespace Emergency.Visit
{
    [Serializable]
    internal class PatientAlreadyOnVisitException : Exception
    {
        public PatientAlreadyOnVisitException()
        {
        }

        public PatientAlreadyOnVisitException(string? message) : base(message)
        {
        }

        public PatientAlreadyOnVisitException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected PatientAlreadyOnVisitException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}