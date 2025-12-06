using Application.Dtos;

namespace Application.Interfaces
{
	public interface IAuthService
	{
		Task<JwtToken?> LoginAsync(string username, string password);
	}
}
