using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
	public class EmployeeService
	{
		public readonly IEmployeeRepository _employeeRepository;

		public EmployeeService(IEmployeeRepository employeeRepository)
		{
			_employeeRepository = employeeRepository;
		}

		public Task<IEnumerable<Employee>> GetAllAsync()
		{
			return _employeeRepository.GetAllAsync();
		}

		public Task<Employee?> GetByIdAsync(int id) { 
			return _employeeRepository.GetByIdAsync(id);
		}

		public async Task<Employee> CreateAsync(Employee employee) {
			if (string.IsNullOrEmpty(employee.Name)) throw new ArgumentNullException("Employee name is required");

			if (employee.Birthdate > DateTime.UtcNow.AddYears(-18)) throw new ArgumentException("Employee must be 18");

			return await _employeeRepository.AddAsync(employee);
		}

		public async Task<bool> UpdateAsync(Employee employee)
		{
			var existing = await _employeeRepository.GetByIdAsync(employee.Id);

			if (existing is null) return false;

			return await _employeeRepository.UpdateAsync(existing);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return _employeeRepository.DeleteAsync(id);
		}
	}
}
