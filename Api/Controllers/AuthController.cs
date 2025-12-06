using Api.Dtos;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;
		
		public AuthController(IAuthService authService)
		{
			_authService = authService;
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
