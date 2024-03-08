﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails
{
	public record GetLeaveAllocationDetailsQuery(int id) : IRequest<LeaveAllocationDetailsDto>
	{
	}
}
