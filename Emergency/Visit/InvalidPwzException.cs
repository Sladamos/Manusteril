using System.Runtime.Serialization;

namespace Emergency.Visit
{
    [Serializable]
    internal class InvalidPwzException : Exception
    {
        public InvalidPwzException()
        {
        }

        public InvalidPwzException(string? message) : base(message)
        {
        }

        public InvalidPwzException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}