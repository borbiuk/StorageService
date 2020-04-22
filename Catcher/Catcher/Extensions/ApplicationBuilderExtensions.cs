using Catcher.Configurations;
using Catcher.Interfaces;

using EasyNetQ;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Catcher.Extensions
{
	internal static class ApplicationBuilderExtensions
	{
		public static IApplicationBuilder UseCatcher(this IApplicationBuilder applicationBuilder)
		{
			var config = applicationBuilder.ApplicationServices.GetService<IOptions<RabbitMqConfig>>().Value;
			var rabbitBus = applicationBuilder.ApplicationServices.GetService<IBus>();
			var catcherService = applicationBuilder.ApplicationServices.GetService<ICatcher>();
			
			rabbitBus.Advanced.Conventions.ErrorQueueNamingConvention = _ =>
				GetErrorQueueName(config.Queue);

			rabbitBus.Receive(config.Queue, registration =>
			{
				catcherService.Register(registration);
			});

			return applicationBuilder;
		}

		private static string GetErrorQueueName(string originalName) =>
			string.IsNullOrEmpty(originalName)
				? $"errors"
				: $"{originalName}.errors";
	}
}
