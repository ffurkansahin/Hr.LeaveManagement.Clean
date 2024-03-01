using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface IGenericRepository<T > where T : class
{
	Task<T> CreateAsync(T entity);
	Task<T> UpdateAsync(T entity);
	Task<T> DeleteAsync(T entity);
	Task<T> GetAsync();
	Task<T> GetByIdAsync(int id);
}

