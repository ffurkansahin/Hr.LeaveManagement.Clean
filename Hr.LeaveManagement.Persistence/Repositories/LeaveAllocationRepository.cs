using Hr.LeaveManagement.Domain;
using Hr.LeaveManagement.Persistence.DbContext;
using HR.LeaveManagement.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hr.LeaveManagement.Persistence.Repositories
{
	public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
	{
		public LeaveAllocationRepository(HrDbContext context) : base(context)
		{
		}

		public async Task AddAllocations(List<LeaveAllocation> allocations)
		{
			await _context.AddRangeAsync(allocations);
		}

		public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
		{
			return await _context.LeaveAllocations.AnyAsync(i => i.EmployeeId == userId
											&& i.LeaveTypeId == leaveTypeId
											&& i.Period == period);
		}

		public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
		{
			var leaveAllocations = await _context.LeaveAllocations
				.Include(i=>i.LeaveType)
				.FirstOrDefaultAsync(i=>i.Id==id);
			return leaveAllocations;
		}

		public async Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails()
		{
			var leaveAllocations = await _context.LeaveAllocations
				.Include(i => i.LeaveType)
				.ToListAsync();
			return leaveAllocations;
		}

		public async Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails(string userId)
		{
			var leaveAllocations = await _context.LeaveAllocations
				.Where(i=>i.EmployeeId==userId)
				.Include(i => i.LeaveType)
				.ToListAsync();
			return leaveAllocations;
		}

		public async Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
		{
			return await _context.LeaveAllocations.FirstOrDefaultAsync(i=>i.EmployeeId==userId
										&& i.LeaveTypeId==leaveTypeId);		
		}
	}
}
