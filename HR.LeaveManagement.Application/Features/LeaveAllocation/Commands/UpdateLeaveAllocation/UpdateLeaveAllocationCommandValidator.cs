using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation
{
	public class UpdateLeaveAllocationCommandValidator : AbstractValidator<UpdateLeaveAllocationCommand>
	{
		private readonly ILeaveTypeRepository _leaveTypeRepository;
		private readonly ILeaveAllocationRepository _leaveAllocationRepository;

		public UpdateLeaveAllocationCommandValidator(ILeaveTypeRepository leaveTypeRepository , ILeaveAllocationRepository leaveAllocationRepository)
        {
			this._leaveTypeRepository = leaveTypeRepository;
			this._leaveAllocationRepository = leaveAllocationRepository;
			RuleFor(i => i.NumberOfDays)
				.GreaterThan(0).WithMessage("{PropertyName} must greater than {ComparisonValue}");

			RuleFor(i => i.Period)
				.GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("{PropertyName} must be after {ComparisonValue}");

			RuleFor(i => i.LeaveTypeId)
				.GreaterThan(0)
				.MustAsync(LeaveTypeMustExist)
				.WithMessage("{PropertyName} does not exist.");

			RuleFor(i => i.Id)
				.NotNull()
				.MustAsync(LeaveAllocationMustExist)
				.WithMessage("{PropertyName} must be present");
		}

		private async Task<bool> LeaveAllocationMustExist(int id, CancellationToken token)
		{
			var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(id);
			return leaveAllocation != null;
		}

		private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
		{
			var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
			return leaveType != null;
		}
	}
}
