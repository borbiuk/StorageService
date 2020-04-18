using System;
using System.Threading.Tasks;
using Catcher.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Catcher
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			var hostBuilder = GetWebHostBuilder()
				.Build();

			await hostBuilder.RunAsync();
		}

		/// <summary>
		/// Returns configured WebHostBuilder
		/// </summary>
		private static IWebHostBuilder GetWebHostBuilder() =>
			new WebHostBuilder()
				.UseKestrel()
				.UseIISIntegration()
				.UseStartup<Startup>()
				.UseConfiguration(GetConfiguration());

		/// <summary>
		/// Get base application configuration
		/// </summary>
		private static IConfiguration GetConfiguration() =>
			new ConfigurationBuilder()
				.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
				.Build();
	}
}
