namespace Application.Dtos
{
	public record JwtToken(string Token, DateTime ExpiresAt);
}
