using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using QueueClient;
using QueueClient.Implementations;

using Recipient.Services.Implementations;
using Recipient.Services.Interfaces;

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
				.AddSingleton<IBackgroundWorkerProvider>(s =>
					new WebBackgroundWorkerProvider(int.Parse(Configuration["Background:WorkersCount"])))
				.AddSingleton<IQueueClient>(s =>
					new RabbitMqClient(new QueueClientOptions
						{
							Host = Configuration["RabbitMq:Host"],
							Username = Configuration["RabbitMq:Username"],
							Password = Configuration["RabbitMq:Password"],
						}));
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
		}
	}
}
