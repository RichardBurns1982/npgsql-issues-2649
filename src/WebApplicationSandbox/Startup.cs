using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplicationSandbox
{
	public class Startup
	{
		public Startup(IConfiguration configuration, IWebHostEnvironment env)
		{
			Configuration = configuration;
			Environment = env;
		}

		private IWebHostEnvironment Environment { get; }
		private IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			var config = Configuration;
			var connectionString = config.GetConnectionString("Connection")!;

			services.AddNpgsqlDataSource(connectionString, options =>
			{
				options.MapComposite<CustomObject>();
			});

			services.AddDbContext<TestDbContext>(options =>
			{
				options.UseNpgsql(connectionString,
				o =>
				{
					o.SetPostgresVersion(PostgresVersion.Version);
					o.MigrationsAssembly($"{nameof(WebApplicationSandbox)}");
				});
			});

			services.AddControllers();
			services.AddCors();
			services.AddHttpClient();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app
				.UseRouting()
				.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin())
				.UseEndpoints(endPoints =>
				{
					endPoints.MapControllers();
				});
		}

	}
}


public static class PostgresVersion
{
	public static readonly Version Version = new(11, 6);
}
