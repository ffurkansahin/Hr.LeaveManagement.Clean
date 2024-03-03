using Hr.LeaveManagement.Domain;
using Hr.LeaveManagement.Persistence.DbContext;
using HR.LeaveManagement.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hr.LeaveManagement.Persistence.Repositories
{
	public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
	{
		public LeaveRequestRepository(HrDbContext context) : base(context)
		{
		}

		public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
		{
			var leaveRequest = await _context.LeaveRequests
				.Include(i => i.LeaveType)
				.FirstOrDefaultAsync(i => i.Id == id);
			return leaveRequest;
		}

		public async Task<List<LeaveRequest>> GetLeaveRequestWithDetails()
		{
			var leaveRequest = await _context.LeaveRequests
				.Include(i => i.LeaveType)
				.ToListAsync();
			return leaveRequest;
		}

		public async Task<List<LeaveRequest>> GetLeaveRequestWithDetails(string userId)
		{
			var leaveRequest = await _context.LeaveRequests
				.Where(i => i.RequestingEmployeeId == userId)
				.Include(i => i.LeaveType)
				.ToListAsync();
			return leaveRequest;
		}
	}
}
