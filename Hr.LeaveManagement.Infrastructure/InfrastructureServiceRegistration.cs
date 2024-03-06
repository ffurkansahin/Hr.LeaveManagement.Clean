using Hr.LeaveManagement.Infrastructure.EmailService;
using Hr.LeaveManagement.Infrastructure.Logging;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Models.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.Infrastructure
{
	public static class InfrastructureServiceRegistration
	{
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
			services.AddTransient<IEmailSender, EmailSender>();

			services.AddScoped(typeof(IAppLogger<>),typeof(LoggerAdapter<>));

			return services;
		}
    }
}
