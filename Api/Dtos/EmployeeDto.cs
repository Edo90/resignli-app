namespace Api.Dtos
{
	public class EmployeeDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public DateTime Birthdate { get; set; }
		public string IdentityNumber { get; set; } = string.Empty;
		public string? Email { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime LastUpdatedAt { get; set; }

	}
}
