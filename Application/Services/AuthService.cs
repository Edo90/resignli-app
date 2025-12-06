using Application.Dtos;
using Application.Interfaces;
using Application.Settings;
using Domain.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
	public class AuthService : IAuthService
	{
		IUserRepository _userRepository;
		private JwtSettings _jwtOptions;

		public AuthService(IUserRepository userRepository, IOptions<JwtSettings> jwtOptions)
		{
			_userRepository = userRepository;
			_jwtOptions = jwtOptions.Value;
		}

		public async Task<JwtToken?> LoginAsync(string username, string password)
		{
			var user = await _userRepository.GetByUserNameAsync(username);

			if (user is null || user.Password != password) return null;

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var expiresAt = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes);

			var token = new JwtSecurityToken(
				issuer: _jwtOptions.Issuer,
				audience: _jwtOptions.Audience,
				expires: expiresAt,
				signingCredentials: credentials,
				claims: new[]
				{
					new Claim(ClaimTypes.Name, user.Username),
					new Claim(ClaimTypes.Role, user.Role!)
				}
				);

			var jwt = new JwtSecurityTokenHandler().WriteToken(token);

			return new JwtToken(jwt,expiresAt);
		}
	}
}
