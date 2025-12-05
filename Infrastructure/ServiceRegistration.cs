using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
	/// <summary>
	/// Class intended just to do the service registration for Dependency Injection
	/// </summary>
	public static class ServiceRegistration
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, string databaseName)
		{

			services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(databaseName));

			services.AddScoped<IEmployeeRepository, EmployeeRepository>();
			services.AddScoped<IUserRepository, UserRepository>();

			return services;

		}
	}
}
