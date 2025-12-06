using Api.Controllers;
using Api.Dtos;
using Application.Dtos;
using Application.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests.Api.Controllers
{
	public class AuthControllerTests
	{
		private readonly Mock<IAuthService> _authMock;
		private readonly AuthController _controller;

		public AuthControllerTests()
		{
			_authMock = new Mock<IAuthService>();
			_controller = new AuthController( _authMock.Object );
		}

		[Fact]
		public async Task Login_ShouldReturnJwt_WhenCredentialsAreValid()
		{
			//Arrage
			var request = new LoginRequestDto { Username = "username", Password = "password" };

			var jwt = new JwtToken(Token: "abc123", ExpiresAt: DateTime.UtcNow.AddMinutes(30));

			_authMock.Setup(serv => serv.LoginAsync("username", "password")).ReturnsAsync(jwt);

			//Act
			var response = await _controller.Login(request);

			//Assert
			var result = response as OkObjectResult;

			result.Should().NotBeNull();
			var objResult = result.Value as LoginResponseDto;

			objResult.Should().NotBeNull();
			objResult!.Token.Should().Be(jwt.Token);
			
		}

		[Fact]
		public async Task Login_ShouldReturnUnauthorized_WhenCredentialsInvalid()
		{
			//Arrange
			var request = new LoginRequestDto { Username = "wrontTest", Password = "password" };

			_authMock.Setup(serv => serv.LoginAsync("wrongTest", "password")).ReturnsAsync((JwtToken?)null);

			//Act
			var response = await _controller.Login(request);
			//Assert

			response.Should().BeOfType<UnauthorizedObjectResult>();
		}
	}
}
