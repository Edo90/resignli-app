using Api.Dtos;
using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly AuthService _authService;
		private readonly IMapper _mapper;

		public AuthController(AuthService authService, IMapper mapper)
		{
			_authService = authService;
			_mapper = mapper;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
		{
			var result = await _authService.LoginAsync(loginRequestDto.Username, loginRequestDto.Password);

			if (result == null) return Unauthorized(new { message = "Invalid username or password, try again." });

			var response = new LoginResponseDto { Token = result.Token, ExpiresAt = result.ExpiresAt };

			return Ok(response);

		}

	}
}
