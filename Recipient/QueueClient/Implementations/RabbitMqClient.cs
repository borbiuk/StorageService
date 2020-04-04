using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EasyNetQ;

namespace QueueClient.Implementations
{
	/// <summary>
	/// IQueueClient implementation that allow to work with RabbitMQ server.
	/// </summary>
	public class RabbitMqClient : IQueueClient, IDisposable
	{
		private readonly QueueClientOptions _options;
		private readonly IBus _rabbitBus;

		public RabbitMqClient(QueueClientOptions options)
		{
			_options = options;
			_rabbitBus = CreateBus(_options);
		}

		public Task SendAsync(string queue, object data) =>
			_rabbitBus.SendAsync(queue, data);

		public void Dispose() => _rabbitBus.Dispose();

		private static IBus CreateBus(QueueClientOptions options) =>
			RabbitHutch.CreateBus(CreateConnectionString(options));

		private static string CreateConnectionString(QueueClientOptions options)
		{
			var parameters =
				new Dictionary<string, string>
				{
					{ "host", options.Host },
					{ "username", options.Username },
					{ "password", options.Password },
					{ "persistentMessages", false.ToString() },
					{ "timeout", "1" }
				}
				.Select(p => string.Concat(p.Key, "=", p.Value));

			return string.Join(";", parameters);
		}
	}
}
