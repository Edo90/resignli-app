using Domain.Entities;

namespace Domain.Interfaces
{
	public interface IUserRepository : IRepository<UserApp>
	{
		Task<UserApp?> GetByUserNameAsync(string username);
	}
}
