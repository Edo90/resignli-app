using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
	public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
	{
		private readonly AppDbContext _appDbContext;
		public EmployeeRepository(AppDbContext db) : base(db)
		{
			_appDbContext = db;
		}

		
	}
}
