using Domain.Entities;
using Domain.Interfaces;

namespace Application.Interfaces
{
	public interface IEmployeeService
	{
		Task<IEnumerable<Employee>> GetAllAsync();
		Task<Employee?> GetByIdAsync(int id);
		Task<Employee> CreateAsync(Employee employee);
		Task<bool> UpdateAsync(Employee employee);
		public Task<bool> DeleteAsync(int id);

	}
}
