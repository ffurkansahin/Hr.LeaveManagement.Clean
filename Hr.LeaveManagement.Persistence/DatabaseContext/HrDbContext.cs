
using Hr.LeaveManagement.Domain;
using Hr.LeaveManagement.Domain.Common;
using Hr.LeaveManagement.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.Persistence.DbContext
{
	public class HrDbContext : Microsoft.EntityFrameworkCore.DbContext
	{
        public HrDbContext(DbContextOptions<HrDbContext> opt) : base(opt)
        {
			   
        }

		public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(HrDbContext).Assembly);

			base.OnModelCreating(modelBuilder);
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
				.Where(i=>i.State == EntityState.Added || i.State == EntityState.Modified))
			{
				entry.Entity.DateModified = DateTime.Now;

				if(entry.State == EntityState.Added)
				{
					entry.Entity.DateCreated = DateTime.Now;
				}

			}
			return base.SaveChangesAsync(cancellationToken);
		}
	}
}
