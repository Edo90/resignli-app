using System.ComponentModel.DataAnnotations;

namespace WebApp.Dtos
{
	public class EmployeeDto
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; } = string.Empty;
		[Required]
		public DateTime Birthdate { get; set; }
		[Required]
		public string IdentityNumber { get; set; } = string.Empty;
		[EmailAddress]
		public string? Email { get; set; }
	}
}
