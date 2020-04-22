namespace Catcher.Configurations
{
	internal class RabbitMqConfig
	{
		/// <summary>
		/// Queue server host.
		/// </summary>
		public string Host { get; set; }

		/// <summary>
		/// Queue name.
		/// </summary>
		public string Queue { get; set; }

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
