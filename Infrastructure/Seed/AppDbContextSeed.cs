using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Seed
{
	public class AppDbContextSeed
	{
		public static async Task SeedAsync(AppDbContext dbContext)
		{
			if (dbContext.Employees.Any()) return;

			dbContext.Employees.AddRange(
				new Employee { IdentityNumber = "111-1111-111", Name = "Pedro", Birthdate = DateTime.Now.AddYears(-18), Email = "test1@email.com", CreatedAt=DateTime.UtcNow },
				new Employee { IdentityNumber = "222-22222-222", Name = "Luis", Birthdate = DateTime.Now.AddYears(-21), Email = "test2@test.com", CreatedAt = DateTime.UtcNow.AddDays(-1) },
				new Employee { IdentityNumber = "333-22222-222", Name = "Torres", Birthdate = DateTime.Now.AddYears(-33), Email = "test3@test.com", CreatedAt = DateTime.UtcNow.AddMonths(-1) }
				);

			dbContext.Users.AddRange(
				new UserApp { Password = "test1@test1@", Role = "dev", Username= "admin", CreatedAt = DateTime.UtcNow },
				new UserApp { Password = "test2@test2@", Role = "sqa", Username = "guest", CreatedAt = DateTime.UtcNow.AddDays(-1) },
				new UserApp { Password = "test3@test3@", Role = "po", Username = "user" , CreatedAt = DateTime.UtcNow.AddDays(-30) }
				);

			await dbContext.SaveChangesAsync();
		}
	}
}
