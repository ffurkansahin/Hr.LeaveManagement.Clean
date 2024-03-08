using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval
{
	public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
	{
		private readonly ILeaveTypeRepository _leaveTypeRepository;
		private readonly ILeaveRequestRepository _leaveRequestRepository;
		private readonly IMapper _mapper;
		private readonly IEmailSender _emailSender;
		private readonly IAppLogger<ChangeLeaveRequestApprovalCommandHandler> _appLogger;

		public ChangeLeaveRequestApprovalCommandHandler(ILeaveTypeRepository leaveTypeRepository, ILeaveRequestRepository leaveRequestRepository, IMapper mapper, IEmailSender emailSender, IAppLogger<ChangeLeaveRequestApprovalCommandHandler> appLogger)
		{
			_leaveTypeRepository = leaveTypeRepository;
			_leaveRequestRepository = leaveRequestRepository;
			_mapper = mapper;
			_emailSender = emailSender;
			_appLogger = appLogger;
		}

		public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
		{
			var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);
			if (leaveRequest is null)
				throw new NotFoundException(nameof(leaveRequest),request.Id);

			leaveRequest.Approved= request.Approved;
			await _leaveRequestRepository.UpdateAsync(leaveRequest);

			//if request is approved get and update the employee's allocation

			try
			{
				var email = new EmailMessage
				{
					To = string.Empty,
					Body = $"The approval status for your leave request for {leaveRequest.StartDate:D} to{leaveRequest.EndDate:D} has been updated succesfully.",
					Subject = "Leave Request Approval Status Updated"
				};
				await _emailSender.SendEmail(email);
			}
			catch (Exception ex)
			{
				_appLogger.LogWarning(ex.Message);
				throw;
			}
			return Unit.Value;

		}
	}
}
