using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using BackgroundProvider;

using QueueClient;
using QueueClient.Implementations;
using Microsoft.OpenApi.Models;

namespace Recipient
{
	internal class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services
				.AddSingleton(s => new BackgroundWorkerProvider(GetBackgroungWorkersCount()))
				.AddSingleton<IQueueClient>(s =>
					new RabbitMqClient(new QueueClientOptions
						{
							Host = Configuration["RabbitMq:Host"],
							Username = Configuration["RabbitMq:Username"],
							Password = Configuration["RabbitMq:Password"],
						}));

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Recipient API", Version = "v1" });
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Recipient API V1");
				c.RoutePrefix = string.Empty;
			});
		}

		private int GetBackgroungWorkersCount()
		{
			const int DefaultCount = 5;
			return int.TryParse(Configuration["Background:WorkersCount"], out var configCount)
				? configCount
				: DefaultCount;
		}
	}
}
