using System.Runtime.Serialization;

namespace Emergency.Visit
{
    [Serializable]
    internal class PatientUnallowedToLeaveException : Exception
    {
        public PatientUnallowedToLeaveException()
        {
        }

        public PatientUnallowedToLeaveException(string? message) : base(message)
        {
        }

        public PatientUnallowedToLeaveException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected PatientUnallowedToLeaveException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}