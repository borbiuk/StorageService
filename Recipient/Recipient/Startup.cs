using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using BackgroundProvider;

using QueueClient;
using QueueClient.Implementations;

namespace Recipient
{
	internal class Startup
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
			services.AddControllers();

			// Configurations init.
			services
				.AddOptions()
				.Configure<QueueClientConfig>(Configuration.GetSection("RabbitMq"));

			// Dependency Injection setup.
			services
				.AddSingleton(s => new BackgroundWorkerProvider(GetBackgroundWorkersCount()))
				.AddSingleton<IQueueClient, RabbitMqQueueClient>();

			// Register the Swagger generator, defining 1 or more Swagger documents.
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "Recipient API",
					Version = "v1"
				});
			});
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

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
			// specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Recipient API v1");
				c.RoutePrefix = string.Empty;
			});
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

		private int GetBackgroundWorkersCount()
		{
			if (int.TryParse(Configuration["Background:WorkersCount"], out var count))
				return count;
			else
				throw new FormatException("Parameter [Background:WorkersCount] is invalid.");
		}
	}
}
