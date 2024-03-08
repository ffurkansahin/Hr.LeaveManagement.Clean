using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hr.LeaveManagement.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LeaveAllocationController : ControllerBase
	{
		private readonly IMediator _mediator;

		public LeaveAllocationController(IMediator mediator)
        {
			this._mediator = mediator;
		}

        // GET: api/<LeaveAllocationController>
        [HttpGet]
		public async Task<ActionResult<List<LeaveAllocationDto>>> Get(bool isLoggedInUser = false)
		{
			var leaveAllocations = _mediator.Send(new GetLeaveAllocationQuery());
			return Ok(leaveAllocations);
		}

		// GET api/<LeaveAllocationController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<LeaveAllocationDto>> Get(int id)
		{
			var leaveAllocationDetails = _mediator.Send(new GetLeaveAllocationDetailsQuery(id));
			return Ok(leaveAllocationDetails);
		}

		// POST api/<LeaveAllocationController>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(400)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Post(CreateLeaveAllocationCommand createLeaveAllocationCommand)
		{
			var response = await _mediator.Send(createLeaveAllocationCommand);
			return CreatedAtAction(nameof(Get), new { id = response });
		}

		// PUT api/<LeaveAllocationController>/5
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(400)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Put(UpdateLeaveAllocationCommand updateLeaveAllocationCommand)
		{
			await _mediator.Send(updateLeaveAllocationCommand);
			return NoContent();
		}

		// DELETE api/<LeaveAllocationController>/5
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(400)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Delete(int id)
		{
			var command = new DeleteLeaveAllocationCommand{ Id = id };
			await _mediator.Send(command);
			return NoContent();
		}
	}
}
