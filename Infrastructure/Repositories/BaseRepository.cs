using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Infrastructure.Repositories
{
	public abstract class BaseRepository<T> : IRepository<T> where T : class, IEntity
	{
		private AppDbContext _db;

		protected BaseRepository(AppDbContext db)
		{
		
			_db = db;
		}

		public async Task<T> AddAsync(T entity)
		{
			await _db.Set<T>().AddAsync(entity);
			await _db.SaveChangesAsync();
			return entity;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var entity = await GetByIdAsync(id);
			if(entity is null) return false;

			_db.Set<T>().Remove(entity);
			await _db.SaveChangesAsync();
			return true;
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _db.Set<T>().ToListAsync();
		}

		public async Task<T?> GetByIdAsync(int id)
		{
			return await _db.Set<T>().FindAsync(id);
		}

		public async Task<bool> UpdateAsync(T entity)
		{
			_db.Set<T>().Update(entity);
			await _db.SaveChangesAsync();
			return true;
		}
	}
}
