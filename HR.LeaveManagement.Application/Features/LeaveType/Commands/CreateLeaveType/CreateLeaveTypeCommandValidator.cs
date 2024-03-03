using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType
{
	public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
	{
		private readonly ILeaveTypeRepository _leaveTypeRepository;

		public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
		{
			_leaveTypeRepository = leaveTypeRepository;

			RuleFor(i => i.Name)
				.NotEmpty().WithMessage("{PropertyName} is required")
				.NotNull().WithMessage("Can not be null")
				.MaximumLength(70).WithMessage("{PropertyName} Must be fewer than 70 characters")
				.MinimumLength(3).WithMessage("{PropertyName} Must be more than 3 characters");
			RuleFor(i => i.DefaultDays)
				.LessThan(100).WithMessage("{PropertyName} cannot exceed 100")
				.GreaterThan(1).WithMessage("{PropertyName} cannot be less than 1");
			RuleFor(i => i)
				.MustAsync(LeaveTypeNameUnique)
				.WithMessage("Leavetype already exists");
			
		}

		private Task<bool> LeaveTypeNameUnique(CreateLeaveTypeCommand command, CancellationToken token)
		{
			return _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
		}
	}
}
