using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataRecipient.Push
{
	public class QueueClient
	{
		private readonly string _queue;
		private readonly IBus _bus;

		public QueueClient(string queueName)
		{
			_queue = queueName;
			_bus = RabbitHutch.CreateBus(GetConnection());
		}

		private static string GetConnection()
		{
			var parameters = new Dictionary<string, string>
			{
				{ "host", Settings.Current.RabbitMQ.Host },
			};
			var result = string.Join(";", parameters.Select(p => string.Concat(p.Key, "=", p.Value)));
			return result;
		}
	}
}
