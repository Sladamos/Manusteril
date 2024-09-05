using System.Runtime.Serialization;

namespace Ward.Room
{
    [Serializable]
    internal class IncorrectRoomException : Exception
    {
        public IncorrectRoomException()
        {
        }

        public IncorrectRoomException(string? message) : base(message)
        {
        }

        public IncorrectRoomException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected IncorrectRoomException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}