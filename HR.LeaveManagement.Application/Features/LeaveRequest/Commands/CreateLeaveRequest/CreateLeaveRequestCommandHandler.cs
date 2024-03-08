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
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest
{
	public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Unit>
	{
		private readonly IMapper _mapper;
		private readonly ILeaveTypeRepository _leaveTypeRepository;
		private readonly ILeaveRequestRepository _leaveRequestRepository;
		private readonly IEmailSender _emailSender;
		private readonly IAppLogger<CreateLeaveRequestCommandHandler> _appLogger;

		public CreateLeaveRequestCommandHandler(IMapper mapper,ILeaveRequestRepository leaveRequestRepository,ILeaveTypeRepository leaveTypeRepository,IEmailSender emailSender,IAppLogger<CreateLeaveRequestCommandHandler> appLogger)
        {
            this._mapper = mapper;
			this._leaveTypeRepository = leaveTypeRepository;
			this._leaveRequestRepository = leaveRequestRepository;
			this._emailSender = emailSender;
			this._appLogger = appLogger;
		}
        public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
		{
			var validator = new CreateLeaveRequestValidator(_leaveTypeRepository);
			var validationResult = await validator.ValidateAsync(request);
			if (validationResult.Errors.Any())
				throw new BadRequestException("Invalid Leave Request", validationResult);

			//Employee id check allocation

			var leaveRequest = _mapper.Map<Hr.LeaveManagement.Domain.LeaveRequest>(request);
			await _leaveRequestRepository.CreateAsync(leaveRequest);

			try
			{
				var email = new EmailMessage
				{
					To = string.Empty,
					Body = $"Your leave request for {request.StartDate:D} to{request.EndDate:D} has been updated succesfully.",
					Subject = "Leave Request Submitted"
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
