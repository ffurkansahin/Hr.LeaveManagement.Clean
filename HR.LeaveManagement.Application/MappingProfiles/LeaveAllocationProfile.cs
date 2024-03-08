using AutoMapper;
using Hr.LeaveManagement.Domain;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.MappingProfiles
{
	public class LeaveAllocationProfile : Profile
	{
        public LeaveAllocationProfile()
        {
            CreateMap<LeaveAllocationDto, LeaveAllocation>().ReverseMap();
            CreateMap<LeaveAllocationDetailsDto, LeaveAllocation>().ReverseMap();
            
        }
    }
}
