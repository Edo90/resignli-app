using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
	public class AppDbContext : DbContext
	{
		public DbSet<Employee> Employees => Set<Employee>();
		public DbSet<UserApp> Users => Set<UserApp>();

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
		
	}
}
