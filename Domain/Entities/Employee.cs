namespace Domain.Entities
{
	public class Employee : BaseEntity
	{
		public required string Name { get; set; }
		public DateTime Birthdate { get; set; }
		public required string IdentityNumber { get; set; }
		public string? Email {  get; set; }
		
	}
}
