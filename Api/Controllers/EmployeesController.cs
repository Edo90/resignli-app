using Api.Dtos;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class EmployeesController : ControllerBase
	{
		private readonly EmployeeService _employeeService;
		private readonly IMapper _mapper;

		public EmployeesController(EmployeeService employeeService, IMapper mapper)
		{
			_employeeService = employeeService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var employees = await _employeeService.GetAllAsync();

			var dtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

			return Ok(dtos);
		}

		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById(int id)
		{
			var employee = await _employeeService.GetByIdAsync(id);

			var dto = _mapper.Map<EmployeeDto>(employee);
			return Ok(dto);
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateEmployeeDto dto)
		{
			var employee = _mapper.Map<Employee>(dto);

			var created = await _employeeService.CreateAsync(employee);

			var result = _mapper.Map<EmployeeDto>(created);

			return CreatedAtAction(nameof(GetById), new {id= created.Id}, result);
		}

		[HttpPut("{id:int}")]
		public async Task<IActionResult> Update(int id, UpdateEmployeeDto dto)
		{
			var employee = await _employeeService.GetByIdAsync(id);

			if (employee is null) return NotFound();

			_mapper.Map(dto,employee);

			var updated = await _employeeService.UpdateAsync(employee);

			return updated ? NoContent() : BadRequest(); 
		}

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			var isDeleted = await _employeeService.DeleteAsync(id);
			return isDeleted ? NoContent() : NotFound();
		}
	}
}
