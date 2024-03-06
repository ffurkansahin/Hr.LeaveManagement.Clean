using Hr.LeaveManagement.Domain;
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
	public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
	{
		public LeaveTypeRepository(HrDbContext context) : base(context)
		{
		}

		public async Task<bool> IsLeaveTypeUnique(string name)
		{
			 return await _context.LeaveTypes.AnyAsync(i=>i.Name == name) == false;
		}
	}
}
