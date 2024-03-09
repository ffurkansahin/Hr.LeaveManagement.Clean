using AutoMapper;
using Hr.LeaveManagement.Application.UnitTest.Mocks;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.MappingProfiles;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.Application.UnitTest.Features.LeaveTypes.Queries
{
	public class GetLeaveTypesListQueryHandlerTest
	{
        private readonly Mock<ILeaveTypeRepository> _mockRepo;
		private IMapper _mapper;
		private Mock<IAppLogger<GetLeaveTypesQueryHandler>> _mockAppLogger;

		public GetLeaveTypesListQueryHandlerTest()
		{
			_mockRepo = MockLeaveTypeRepository.GetLeaveTypeMock();
			var mapperConfig = new MapperConfiguration(c =>
			{
				c.AddProfile<LeaveTypeProfile>();
			});

			_mapper = mapperConfig.CreateMapper();
			_mockAppLogger = new Mock<IAppLogger<GetLeaveTypesQueryHandler>>();
		}
		[Fact]
		public async Task TestGetLeaveTypeList()
		{
			var handler = new GetLeaveTypesQueryHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);

			var result = await handler.Handle(new GetLeaveTypesQuery(),CancellationToken.None);

			result.ShouldBeOfType<List<LeaveTypeDto>>();
			result.Count.ShouldBe(3);

		}

	}
}
