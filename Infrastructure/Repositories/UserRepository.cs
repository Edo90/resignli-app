using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
	public class UserRepository : BaseRepository<UserApp>, IUserRepository
	{
		private readonly AppDbContext _context;
		public UserRepository(AppDbContext db) : base(db)
		{
			_context = db;
		}

		public async Task<UserApp?> GetByUserName(string username)
		{
			return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
		}
	}
}
