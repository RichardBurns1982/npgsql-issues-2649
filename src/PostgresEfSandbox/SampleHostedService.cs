using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgresEfSandbox
{
	internal class SampleHostedService : IHostedService
	{
		private readonly IServiceProvider services;

		public SampleHostedService(
			IServiceProvider services)
		{
			this.services = services;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			for (int i = 0; i < 2; i++)
			{
				using (var scope = services.CreateScope())
				{
					var testDbContext =
						scope.ServiceProvider
							.GetRequiredService<TestDbContext>();


					if (testDbContext.Database.GetDbConnection() is not NpgsqlConnection connection)
					{
						throw new InvalidOperationException("Connection is not NpgsqlConnection");
					}
				}
			}
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{

			return Task.CompletedTask;
		}
	}
}
