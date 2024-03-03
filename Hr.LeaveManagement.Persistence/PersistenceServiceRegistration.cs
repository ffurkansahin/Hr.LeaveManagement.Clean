using Hr.LeaveManagement.Persistence.DbContext;
using Hr.LeaveManagement.Persistence.Repositories;
using HR.LeaveManagement.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.Persistence
{
	public static class PersistenceServiceRegistration
	{
		public static IServiceCollection AddPersistenceServices(this ServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<HrDbContext>(opt =>
			{
				opt.UseSqlServer(configuration.GetConnectionString("HrDatabaseConnectionString"));
			});
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
			services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
			services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();

			return services;
		}
	}
}
