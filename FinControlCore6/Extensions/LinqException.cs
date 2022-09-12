namespace FinControlCore6.Extensions
{

	[Serializable]
	public class LinqException : Exception
	{
		public LinqException() { }
		public LinqException(string message) : base(message) { }
		public LinqException(string message, Exception inner) : base(message, inner) { }
		protected LinqException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
