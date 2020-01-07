using Microsoft.Extensions.Configuration;
using System;

namespace DataRecipient.Push
{
	internal class Settings
	{
		private static readonly Lazy<AppSettingsOptions> ConfigurationProvider = new Lazy<AppSettingsOptions>(() =>
			new ConfigurationBuilder()
				.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
				.AddJsonFile("appsettings.json", true, true)
				.Build()
				.Get<AppSettingsOptions>());

		public static AppSettingsOptions Current => ConfigurationProvider.Value ?? throw new InvalidOperationException("AppSettings is null!");

		internal class AppSettingsOptions
		{
			public RabbitSettings RabbitMQ { get; set; }
		}

		internal class RabbitSettings
		{
			public string Host { get; set; }

			public string Queue { get; set; }
		}
	}
}
