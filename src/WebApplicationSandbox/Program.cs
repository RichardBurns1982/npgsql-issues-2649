using WebApplicationSandbox;

namespace Smartwyre;

public class Program
{
	public static int Main(string[] args)
	{
		CreateHostBuilder(args).Build().Run();
		return 0;
	}

	public static IHostBuilder CreateHostBuilder(string[] args) =>
		Host.CreateDefaultBuilder(args)
			.ConfigureWebHostDefaults(hostBuilder =>
			{
				hostBuilder
					.UseStartup<Startup>();
			});
}
