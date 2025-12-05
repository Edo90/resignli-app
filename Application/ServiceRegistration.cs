using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddApplication(this IServiceCollection services) {
			services.AddScoped<EmployeeService>();
			services.AddScoped<AuthService>();

			return services;
		}
	}
}
