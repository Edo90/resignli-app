using Application.Dtos;
using Application.Settings;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Application.Services
{
	public class AuthService
	{
		IUserRepository _userInterface;
		private JwtSettings _jwtOptions;

		public AuthService(IUserRepository userInterface, IOptions<JwtSettings> jwtOptions)
		{
			_userInterface = userInterface;
			_jwtOptions = jwtOptions.Value;
		}

		//public async Task<LoginResponse?> LoginAsync(LoginRequest loginRequest)
		//{
		//	//show check for user and generate token

		//	var user = await _userInterface.GetByUserName(loginRequest.Username);

		//	if (user is null || user.Password != loginRequest.Password) return null;

		//	var token = GenerateJwtToken(user);
		//}

		//private JwtToken GenerateJwtToken(UserApp user)
		//{
		//	var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
		//	var token = new JwtSecurityToken(issuer: _jwtOptions.Issuer, audience: _jwtOptions.Audience, expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes));
		//}
	}

	record JwtToken
	{
		string token;
		string expiresAt;
	}
}
