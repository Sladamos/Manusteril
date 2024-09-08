using System.Runtime.Serialization;

namespace Ward.Visit.Question
{
    [Serializable]
    internal class QuestionMissingException : Exception
    {
        public QuestionMissingException()
        {
        }

        public QuestionMissingException(string? message) : base(message)
        {
        }

        public QuestionMissingException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected QuestionMissingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}