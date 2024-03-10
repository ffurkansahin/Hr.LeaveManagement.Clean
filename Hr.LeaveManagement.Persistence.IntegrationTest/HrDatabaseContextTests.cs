using Hr.LeaveManagement.Domain;
using Hr.LeaveManagement.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Hr.LeaveManagement.Persistence.IntegrationTest
{
	public class HrDatabaseContextTests
	{
		private readonly HrDbContext _hrDatabaseContext;

        public HrDatabaseContextTests()
        {
            var DbOptions = new DbContextOptionsBuilder<HrDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

			_hrDatabaseContext = new HrDbContext(DbOptions);
        }

        [Fact]
		public async void Save_SetDateCreatedValue()
		{
			//Arrange 
			var leaveType = new LeaveType
			{
				Id = 1,
				DefaultDays = 10,
				Name = "TestVacation"
			};

			//Act
			await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
			await _hrDatabaseContext.SaveChangesAsync();

			//Assert
			leaveType.DateCreated.ShouldNotBeNull();

		}

		[Fact]
		public async Task Save_SetDateModifiedValueAsync()
		{
			//Arrange 
			var leaveType = new LeaveType
			{
				Id = 1,
				DefaultDays = 10,
				Name = "TestVacation"
			};

			//Act
			await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
			await _hrDatabaseContext.SaveChangesAsync();

			//Assert
			leaveType.DateModified.ShouldNotBeNull();
		}
	}
}