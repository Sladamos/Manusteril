using System.Runtime.Serialization;

namespace Emergency.Visit
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
    }
}