using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddApplication(this IServiceCollection services) {
			services.AddScoped<IEmployeeService,EmployeeService>();
			services.AddScoped<IAuthService,AuthService>();

			return services;
		}
	}
}
