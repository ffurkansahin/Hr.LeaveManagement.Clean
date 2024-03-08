using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Shared
{
	public class BaseLeaveRequestValidator : AbstractValidator<BaseLeaveRequest> 
	{
		private readonly ILeaveTypeRepository _leaveTypeRepository;

		public BaseLeaveRequestValidator(ILeaveTypeRepository leaveTypeRepository)
        {
			this._leaveTypeRepository = leaveTypeRepository;

			RuleFor(i => i.StartDate)
				.LessThan(i => i.EndDate).WithMessage("{PropertyName} must be before {ComprasionValue}");

			RuleFor(i => i.EndDate)
				.GreaterThan(i => i.StartDate).WithMessage("{PropertyName} must be after {ComprasionValue}");

			RuleFor(i => i.LeaveTypeId)
				.GreaterThan(0)
				.MustAsync(LeaveTypeMustExist)
				.WithMessage("{PropertyName} does not exist");
		}

		private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
		{
			var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
			return leaveType != null;
		}
	}
}
