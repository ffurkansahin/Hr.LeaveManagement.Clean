using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails
{
	public class GetLeaveTypeDetailsQueryHandler : IRequestHandler<GetLeaveTypeDetailsQuery, LeaveTypeDetailsDto>
	{
		private readonly IMapper _mapper;
		private readonly ILeaveTypeRepository _leaveTypeRepository;

		public GetLeaveTypeDetailsQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
		{
			this._mapper = mapper;
			this._leaveTypeRepository = leaveTypeRepository;
		}

		public async Task<LeaveTypeDetailsDto> Handle(GetLeaveTypeDetailsQuery request, CancellationToken cancellationToken)
		{
			var leaveTypeDetails = await _leaveTypeRepository.GetByIdAsync(request.id);

			//verify data exists
			if (leaveTypeDetails == null)
				throw new NotFoundException(nameof(LeaveType), request.id);

			var data = _mapper.Map<LeaveTypeDetailsDto>(leaveTypeDetails);

			return data;
		}
	}
}
