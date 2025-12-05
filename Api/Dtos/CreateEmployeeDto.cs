namespace Api.Dtos
{
	public class CreateEmployeeDto
	{
		public string Name { get; set; } = string.Empty;
		public DateTime BirthDate { get; set; }
		public string IdentityNumber { get; set; } = string.Empty;
		public string? Email { get; set; }

		
	}
}
