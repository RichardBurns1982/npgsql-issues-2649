using Microsoft.EntityFrameworkCore;

namespace PostgresEfSandbox
{
	public class TestDbContext : DbContext
	{
		public TestDbContext(DbContextOptions<TestDbContext> options)
			: base(options)
		{
		}
	}
}
