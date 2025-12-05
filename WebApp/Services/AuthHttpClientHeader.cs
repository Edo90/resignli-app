
namespace WebApp.Services
{
	public class AuthHttpClientHeader : DelegatingHandler
	{
		private readonly ITokenStorage _tokenStorage;

		public AuthHttpClientHeader(ITokenStorage tokenStorage)
		{
			_tokenStorage = tokenStorage;
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var token = await _tokenStorage.GetTokenAsync();

			if (!string.IsNullOrWhiteSpace(token))
			{
				request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
			} 
			return await base.SendAsync(request, cancellationToken);
		}
	}
}
