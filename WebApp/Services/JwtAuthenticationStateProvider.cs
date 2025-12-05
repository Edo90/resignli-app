using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebApp.Services
{
	public class JwtAuthenticationStateProvider : AuthenticationStateProvider
	{
		private readonly ITokenStorage _tokenStorage;

		public JwtAuthenticationStateProvider(ITokenStorage tokenStorage)
		{
			_tokenStorage = tokenStorage;
		}

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var token = await _tokenStorage.GetTokenAsync();

			if (string.IsNullOrWhiteSpace(token)) return new AuthenticationState(new System.Security.Claims.ClaimsPrincipal(new ClaimsIdentity()));

			var handler = new JwtSecurityTokenHandler();
			var jwt = handler.ReadJwtToken(token);

			var identity = new ClaimsIdentity(jwt.Claims, "jwt");
			var user = new ClaimsPrincipal(identity);

			return new AuthenticationState(user);
		}

		public async Task MarkUserAsAuthenticated(string token)
		{
			await _tokenStorage.SaveTokenAsync(token);
			NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
		}

		public async Task MarUserAsLoggedOut()
		{
			await _tokenStorage.ClearTokenAsync();
			NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
		}
	}
}
