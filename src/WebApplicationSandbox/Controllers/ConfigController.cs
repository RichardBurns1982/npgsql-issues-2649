using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Text;

namespace WebApplicationSandbox.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ConfigController : ControllerBase
	{
		private readonly ILogger<ConfigController> _logger;
		private readonly NpgsqlDataSource npgsqlDataSource;
		private readonly TestDbContext testDbContext;

		public ConfigController(
			ILogger<ConfigController> logger,
			NpgsqlDataSource npgsqlDataSource,
			TestDbContext context)
		{
			_logger = logger;
			this.npgsqlDataSource = npgsqlDataSource;
			this.testDbContext = context;
		}

		[HttpGet]
		public IActionResult Get()
		{
			if (testDbContext.Database.GetDbConnection() is not NpgsqlConnection connection)
			{
				throw new InvalidOperationException("Connection is not NpgsqlConnection");
			}



			StringBuilder sb = new StringBuilder();
			sb.Append("Connection loaded");
			sb.Append($"Number of UserTypeMappings from EF: ");

			return Ok(sb.ToString());
		}
	}
}