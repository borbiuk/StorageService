using Microsoft.OpenApi.Models;
using API.Services.DataServices;
using AutoMapper;
using DAL;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using FluentValidation;
using API.TransferData;
using API.TransferData.Validators;

namespace WebApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
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
			services.AddControllers()
				.AddFluentValidation(validator =>
				{
					validator.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
					validator.ImplicitlyValidateChildProperties = true;
				});

			services.AddAutoMapper(typeof(Startup));

			// Dependency Injection setup.
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IDataAccessService, DataAccessService>();
			services.AddTransient<IValidator<EntityDto>, EntityDtoValidator>();

			// Connect to Database.
			services.AddDbContext<ApplicationContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("SqlDatabase")));

			// Register the Swagger generator, defining 1 or more Swagger documents.
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo { Title = "Data Access Api swagger", Version = "v1" });
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

			app.UseAuthorization();

			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
			// specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Data Access Api swagger v1");
				c.RoutePrefix = string.Empty;
			});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
