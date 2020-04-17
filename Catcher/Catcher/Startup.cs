using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Catcher
{
	internal class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		/// <summary>
		/// Set of configuration properties
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
		}
	}
}
