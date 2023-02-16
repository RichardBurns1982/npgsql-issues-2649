using Microsoft.EntityFrameworkCore;
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
		private readonly NpgsqlDataSource npgsqlDataSource;
		private readonly TestDbContext testDbContext;

		public SampleHostedService(
			NpgsqlDataSource npgsqlDataSource,
			TestDbContext testDbContext)
		{
			this.npgsqlDataSource = npgsqlDataSource;
			this.testDbContext = testDbContext;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			if (testDbContext.Database.GetDbConnection() is not NpgsqlConnection connection)
			{
				throw new InvalidOperationException("Connection is not NpgsqlConnection");
			}

			await using var npgsqlDataSourceConnection = await npgsqlDataSource.OpenConnectionAsync();
			

		}

		public Task StopAsync(CancellationToken cancellationToken)
		{

			return Task.CompletedTask;
		}
	}
}
