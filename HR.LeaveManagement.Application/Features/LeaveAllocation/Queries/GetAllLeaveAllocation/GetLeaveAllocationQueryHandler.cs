using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocation
{
	public class GetLeaveAllocationQueryHandler : IRequestHandler<GetLeaveAllocationQuery, List<LeaveAllocationDto>>
	{
		private readonly IMapper _mapper;
		private readonly ILeaveAllocationRepository _leaveAllocationRepository;

		public GetLeaveAllocationQueryHandler(IMapper mapper,ILeaveAllocationRepository leaveAllocationRepository)
        {
			this._mapper = mapper;
			this._leaveAllocationRepository = leaveAllocationRepository;
		}

        public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationQuery request, CancellationToken cancellationToken)
		{
			var leaveAllocations = await _leaveAllocationRepository.GetLeaveAllocationWithDetails();
			var allocations = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);
			return allocations;
		}
	}
}
