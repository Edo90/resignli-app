using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Tests.Application.Services
{
	public class EmployeeServiceTests
	{
		private readonly Mock<IEmployeeRepository> _repoMock;

		private readonly EmployeeService _employeeService;

		public EmployeeServiceTests()
		{
			_repoMock = new Mock<IEmployeeRepository>();
			_employeeService = new EmployeeService(_repoMock.Object);
		}

		[Fact]
		public async Task CreateAsync_ShouldThrow_WhenEmployeeIsUnder18()
		{
			var employee = new Employee
			{
				Name = "John",
				IdentityNumber = "123abc",
				Birthdate = DateTime.UtcNow.AddYears(-10)
			};

			Func<Task> act = async () => await _employeeService.CreateAsync(employee);

			await act.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task GetAllAsync_ShouldReturnEmployees()
		{
			var list = new List<Employee>
			{
				new() { Id = 1,IdentityNumber = "123abc", Name = "John" },
				new() { Id = 2, IdentityNumber = "123def",Name = "Maria" }
			};

			_repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(list);

			var result = await _employeeService.GetAllAsync();

			result.Should().HaveCount(2);
		}


	}
}
