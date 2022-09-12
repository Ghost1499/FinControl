namespace FinControlCore6.Exceptions
{
    [Serializable]
    public class FinControlBaseException : Exception
    {
        public FinControlBaseException() { }
        public FinControlBaseException(string message) : base(message) { }
        public FinControlBaseException(string message, Exception inner) : base(message, inner) { }
        protected FinControlBaseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
