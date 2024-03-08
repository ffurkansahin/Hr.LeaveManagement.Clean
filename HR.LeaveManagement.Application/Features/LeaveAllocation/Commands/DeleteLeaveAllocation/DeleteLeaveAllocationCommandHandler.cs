using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation
{
	public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand, Unit>
	{
		private readonly IMapper _mapper;
		private readonly ILeaveAllocationRepository _leaveAllocation;

		public DeleteLeaveAllocationCommandHandler(IMapper mapper,ILeaveAllocationRepository leaveAllocation) 
        {
			this._mapper = mapper;
			this._leaveAllocation = leaveAllocation;
		}
        public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
		{
			var leaveAllocation = await _leaveAllocation.GetByIdAsync(request.Id);
			if (leaveAllocation == null)
				throw new NotFoundException(nameof(leaveAllocation),request.Id);

			await _leaveAllocation.DeleteAsync(leaveAllocation);
			return Unit.Value;
		}
	}
}
