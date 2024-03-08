using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails
{
	public class GetLeaveRequestDetailsQueryHandler : IRequestHandler<GetLeaveRequestDetailQuery, LeaveRequestDetailsDto>
	{
		private readonly IMapper _mapper;
		private readonly ILeaveRequestRepository _leaveRequestRepository;

		public GetLeaveRequestDetailsQueryHandler(IMapper mapper,ILeaveRequestRepository leaveRequestRepository)
        {
			this._mapper = mapper;
			this._leaveRequestRepository = leaveRequestRepository;
		}
        public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailQuery request, CancellationToken cancellationToken)
		{
			var leaveRequest = _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);
			var data = _mapper.Map<LeaveRequestDetailsDto>(leaveRequest);

			//Add employee details as needed

			return data;
		}
	}
}
