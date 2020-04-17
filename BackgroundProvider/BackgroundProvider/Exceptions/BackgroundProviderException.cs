using System;

namespace BackgroundProvider.Exceptions
{
	public class BackgroundProviderException : Exception
	{
		public BackgroundProviderException(string message)
			: base(message)
		{
		}

		public BackgroundProviderException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
