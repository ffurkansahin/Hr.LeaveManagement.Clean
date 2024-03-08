using Hr.LeaveManagement.Domain;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest
{
	public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, Unit>
	{
		private readonly ILeaveRequestRepository _leaveRequest;

		public DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequest)
        {
			this._leaveRequest = leaveRequest;
		}
        public async Task<Unit> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
		{
			var leaveRequest = await _leaveRequest.GetByIdAsync(request.Id);
			if (leaveRequest == null)
				throw new NotFoundException(nameof(Hr.LeaveManagement.Domain.LeaveRequest), request.Id);

			await _leaveRequest.DeleteAsync(leaveRequest);
			return Unit.Value;

		}
	}
}
