using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PostgresEfSandbox;

try
{
	var host = Host.CreateDefaultBuilder(args)
		.ConfigureAppConfiguration((host, config) =>
		{
			config.AddUserSecrets<Program>(optional: true, reloadOnChange: false);
		})
		.ConfigureServices((context, services) =>
		{
			var config = context.Configuration;
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
					o.MigrationsAssembly($"{nameof(PostgresEfSandbox)}");
				});
			});

			services.AddHostedService<SampleHostedService>();
		});

	await host.RunConsoleAsync();
}
catch (Exception ex)
{
	Console.WriteLine(ex.ToString());
	Console.WriteLine("An unexpected exception occurred and the application cannot continue. Migration Failed");
}


public static class PostgresVersion
{
	public static readonly Version Version = new(11, 6);
}
