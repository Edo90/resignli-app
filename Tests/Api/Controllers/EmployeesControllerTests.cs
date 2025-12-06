using Api.Controllers;
using Api.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests.Api.Controllers
{
	public class EmployeesControllerTests
	{
		private readonly Mock<IEmployeeService> _serviceMock;
		private readonly Mock<IMapper> _mapperMock;
		private readonly EmployeesController _employeesController;

		public EmployeesControllerTests()
		{
			_serviceMock = new();
			_mapperMock = new();
			_employeesController = new EmployeesController(_serviceMock.Object, _mapperMock.Object);
		}

		[Fact]
		public async Task GetById_ShouldReturnEmployee_WhenFound()
		{
			//arrange

			var employee = new Employee { Id = 1, Name = "Test", IdentityNumber = "123" };
			var dto = new EmployeeDto { Id = 1, Name = "Test", IdentityNumber = "123" };

			_serviceMock.Setup(serv => serv.GetByIdAsync(1)).ReturnsAsync(employee);
			_mapperMock.Setup(map => map.Map<EmployeeDto>(employee)).Returns(dto);

			//act
			var response = await _employeesController.GetById(1);

			//assert
			var result = response as OkObjectResult;
			result.Should().NotBeNull();
			result!.Value.Should().BeEquivalentTo(dto);
		}

		[Fact]
		public async Task GetById_ShouldReturnNotFound_WhenMissing()
		{
			//arrange
			_serviceMock.Setup(serv => serv.GetByIdAsync(1)).ReturnsAsync((Employee?)null);

			//act
			var response = await _employeesController.GetById(1);

			//assert
			response.Should().BeOfType<NotFoundResult>();
		}

	}
}
