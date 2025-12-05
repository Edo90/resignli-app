using System.Net.Http.Json;
using WebApp.Dtos;
using WebApp.Services;

namespace WebApp.Clients
{
	public class AuthClient
	{
		private readonly HttpClient _http;
		private readonly JwtAuthenticationStateProvider _authProvider;

		public AuthClient(IHttpClientFactory httpClient, JwtAuthenticationStateProvider authProvider)
		{
			_http = httpClient.CreateClient("ApiClient");
			_authProvider = authProvider;
		}

		public async Task<bool> LoginAsync(string username, string password)
		{
			var response = await _http.PostAsJsonAsync("api/auth/login", new {Username = username, Password = password});

			if (!response.IsSuccessStatusCode) return false;

			var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

			if(result is null) return false;

			await _authProvider.MarkUserAsAuthenticated(result.Token);
			return true;
		}

		public async Task LogoutAsync()
		{
			await _authProvider.MarUserAsLoggedOut();
		}
	}
}
