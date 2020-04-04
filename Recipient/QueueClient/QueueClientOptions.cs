namespace QueueClient
{
	public class QueueClientOptions
	{
		/// <summary>
		/// Queue server host.
		/// </summary>
		public string Host { get; set; }

		/// <summary>
		/// User name to login.
		/// </summary>
		public string Username { get; set; }

		/// <summary>
		/// Password that get access to the queue server.
		/// </summary>
		public string Password { get; set; }
	}
}
