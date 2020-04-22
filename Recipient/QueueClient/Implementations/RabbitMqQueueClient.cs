using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EasyNetQ;

using Microsoft.Extensions.Options;

namespace QueueClient.Implementations
{
	/// <summary>
	/// IQueueClient implementation that allow to work with RabbitMQ server.
	/// </summary>
	public class RabbitMqQueueClient : IQueueClient, IDisposable
	{
		private readonly string _queue;
		private readonly IBus _rabbitBus;

		public RabbitMqQueueClient(IOptions<QueueClientConfig> options)
		{
			var config = options.Value;

			_queue = config.Queue;
			_rabbitBus = GetBus(config);
		}

		/// <summary>
		/// Send data to the queue.
		/// </summary>
		/// <param name="queue">Queue name.</param>
		/// <param name="data">Data to send.</param>
		public Task SendAsync(object data) =>
			_rabbitBus.SendAsync(_queue, data);

		/// <summary>
		/// Free managed objects.
		/// </summary>
		public void Dispose() => _rabbitBus.Dispose();

		/// <summary>
		/// Create and returns new Rabbit MQ bus by configurations.
		/// </summary>
		/// <param name="config"></param>
		private static IBus GetBus(QueueClientConfig config) =>
			RabbitHutch.CreateBus(CreateConnectionString(config));

		/// <summary>
		/// Create and returns connection string for Rabbit MQ server.
		/// </summary>
		private static string CreateConnectionString(QueueClientConfig config)
		{
			var parameters =
				new Dictionary<string, string>
				{
					{ "host", config.Host },
					{ "username", config.Username },
					{ "password", config.Password },
					{ "persistentMessages", false.ToString() },
					{ "timeout", "1" }
				}
				.Select(p => string.Concat(p.Key, "=", p.Value));

			return string.Join(";", parameters);
		}
	}
}
