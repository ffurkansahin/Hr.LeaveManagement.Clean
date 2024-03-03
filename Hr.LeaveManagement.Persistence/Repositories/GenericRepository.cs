
using Hr.LeaveManagement.Domain.Common;
using Hr.LeaveManagement.Persistence.DbContext;
using HR.LeaveManagement.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.Persistence.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		protected readonly HrDbContext _context;

		public GenericRepository(HrDbContext context)
        {
			this._context = context;
		}

		public async Task CreateAsync(T entity)
		{
			await _context.AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(T entity)
		{
			_context.Remove(entity);
			await _context.SaveChangesAsync();
		}

		public async Task<IReadOnlyList<T>> GetAsync()
		{
			return await _context.Set<T>().AsNoTracking().ToListAsync();	
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(i=>i.Id == id);
		}

		public async Task UpdateAsync(T entity)
		{
			_context.Update(entity);
			//_context.Entry(entity).State=EntityState.Modified; we did this by overriding SaveChangesAsync method
			await _context.SaveChangesAsync();
		}
	}
}
