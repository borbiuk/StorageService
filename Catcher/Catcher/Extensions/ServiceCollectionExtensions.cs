using System.Collections.Generic;
using System.Linq;

using Catcher.Configurations;

using EasyNetQ;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Catcher.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddRabbitMqBus(this IServiceCollection services) =>
			services.AddSingleton(serviceProvider =>
			{
				var options = serviceProvider.GetService<IOptions<RabbitMqConfig>>();
				return RabbitHutch.CreateBus(CreateConnectionString(options.Value));
			});

		private static string CreateConnectionString(RabbitMqConfig config)
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
