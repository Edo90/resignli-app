namespace Domain.Entities
{
	public class UserApp : BaseEntity
	{
		public string Username { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string? Role { get; set; }
	}
}
