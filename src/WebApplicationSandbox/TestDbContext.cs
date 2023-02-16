using Microsoft.EntityFrameworkCore;

namespace WebApplicationSandbox
{
	public class TestDbContext : DbContext
	{
		public TestDbContext(DbContextOptions<TestDbContext> options)
			: base(options)
		{
		}
	}
}
