
using Microsoft.JSInterop;

namespace WebApp.Services
{
	public class SessionTokenStorage : ITokenStorage
	{
		private readonly IJSRuntime _js;

		public SessionTokenStorage(IJSRuntime js)
		{
			_js = js;
		}

		public Task ClearTokenAsync()
		{
			return _js.InvokeVoidAsync("sessionStorage.removeItem", "jwt").AsTask();
		}

		public async Task<string?> GetTokenAsync()
		{
			return await _js.InvokeAsync<string>("sessionStorage.getItem", "jwt");
		}

		public Task SaveTokenAsync(string token)
		{
			return _js.InvokeVoidAsync("sessionStorage.setItem", "jwt",token).AsTask();
		}
	}
}
