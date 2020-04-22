using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using System.Threading.Tasks;

namespace Recipient
{
	internal class Program
	{
		public static async Task Main()
		{
			await CreateHostBuilder()
				.Build()
				.RunAsync();
		}

		/// <summary>
		/// Returns configured WebHostBuilder.
		/// </summary>
		private static IHostBuilder CreateHostBuilder() =>
			Host.CreateDefaultBuilder()
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
