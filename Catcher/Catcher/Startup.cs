using Catcher.Configurations;
using Catcher.Extensions;
using Catcher.Implementations;
using Catcher.Interfaces;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Catcher
{
	public class Startup
	{
		public Startup(IWebHostEnvironment env)
		{
			Configuration = GetConfiguration(env);
		}

		/// <summary>
		/// Set of configuration properties.
		/// </summary>
		public IConfiguration Configuration { get; }

		/// <summary>
		/// Add services to the container.
		/// </summary>
		/// <remarks>
		/// This method gets called by the runtime.
		/// </remarks>
		public void ConfigureServices(IServiceCollection services)
		{
			// Configurations init.
			services
				.AddOptions()
				.Configure<DataAccessWebApiConfig>(Configuration.GetSection("DataAccessWebApi"))
				.Configure<RabbitMqConfig>(Configuration.GetSection("RabbitMq"));

			// Dependency Injection setup.
			services
				.AddSingleton<IDataHandler, DataAccessWebApiHandler>()
				.AddSingleton<ICatcher, CatcherService>();

			// Configure RabbitMQ bus.
			services.AddRabbitMqBus();
		}

		/// <summary>
		/// Configure the HTTP request pipeline.
		/// </summary>
		/// <remarks>
		/// This method gets called by the runtime.
		/// </remarks>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCatcher();
		}

		private static IConfiguration GetConfiguration(IWebHostEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json",
							optional: false,
							reloadOnChange: true);

			if (env.IsDevelopment())
			{
				builder.AddUserSecrets<Startup>();
			}

			return builder.Build();
		}
	}
}
