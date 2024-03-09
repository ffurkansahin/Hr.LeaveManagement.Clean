using Hr.LeaveManagement.Domain;
using HR.LeaveManagement.Application.Contracts.Persistence;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.Application.UnitTest.Mocks
{
	public class MockLeaveTypeRepository
	{
		public static Mock<ILeaveTypeRepository> GetLeaveTypeMock()
		{
			var leaveTypes = new List<LeaveType>
			{
				new LeaveType
				{
					Id = 1,
					DefaultDays = 10,
					Name = "Test Vacation"
				},

				new LeaveType
				{
					Id = 2,
					DefaultDays = 20,
					Name = "Test Sick"
				},

				new LeaveType
				{
					Id = 3,
					DefaultDays = 15,
					Name = "Test Maternity"
				},
			};

			var mockRepo = new Mock<ILeaveTypeRepository>();

			mockRepo.Setup(i => i.GetAsync()).ReturnsAsync(leaveTypes);
			mockRepo.Setup(i => i.CreateAsync(It.IsAny<LeaveType>())).Returns((LeaveType leaveType) => 
			{ 
				leaveTypes.Add(leaveType);
				return Task.CompletedTask;
			});

			return mockRepo;
		}
	}
}
