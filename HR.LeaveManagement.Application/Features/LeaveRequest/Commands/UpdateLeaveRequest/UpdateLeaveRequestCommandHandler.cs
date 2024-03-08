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
using System.Threading.Tasks.Sources;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest
{
	public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
	{
		private readonly IMapper _mapper;
		private readonly ILeaveRequestRepository _leaveRequestRepository;
		private readonly IEmailSender _emailSender;
		private readonly ILeaveTypeRepository _leaveTypeRepository;
		private readonly Contracts.Logging.IAppLogger<UpdateLeaveRequestCommandHandler> _appLogger;

		public UpdateLeaveRequestCommandHandler(IMapper mapper,ILeaveRequestRepository leaveRequestRepository,IEmailSender emailSender,ILeaveTypeRepository leaveTypeRepository,IAppLogger<UpdateLeaveRequestCommandHandler> appLogger)
        {
			this._mapper = mapper;
			this._leaveRequestRepository = leaveRequestRepository;
			this._emailSender = emailSender;
			this._leaveTypeRepository = leaveTypeRepository;
			this._appLogger = appLogger;
		}
        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
		{
			var leaveRequest =await _leaveRequestRepository.GetByIdAsync(request.Id);
			if (leaveRequest is null) 
				throw new NotFoundException(nameof(leaveRequest),request.Id);

			var validator = new UpdateLeaveRequestCommandValidator(_leaveTypeRepository,_leaveRequestRepository);
			var validationResult = await validator.ValidateAsync(request);
			if (validationResult.Errors.Any())
				throw new BadRequestException("Invalid Leave Request", validationResult);

			_mapper.Map(request, leaveRequest);

			await _leaveRequestRepository.UpdateAsync(leaveRequest);

			try
			{
				var email = new EmailMessage
				{
					To = string.Empty,
					Body = $"Your leave request for {request.StartDate:D} to{request.EndDate:D} has been updated succesfully.",
					Subject = "Leave Request Updated"
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
