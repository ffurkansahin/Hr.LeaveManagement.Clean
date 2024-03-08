using Microsoft.AspNetCore.Mvc;

namespace Hr.LeaveManagement.Api.Models
{
	public class CustomProblemDetails : ProblemDetails
	{
		public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
	}
}
