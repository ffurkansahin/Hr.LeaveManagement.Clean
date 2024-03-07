using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hr.LeaveManagement.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LeaveTypesController : ControllerBase
	{
		private readonly IMediator _mediator;

		public LeaveTypesController(IMediator mediator)
        {
			this._mediator = mediator;
		}
        // GET: api/<LeaveTypesController>
        [HttpGet]
		public async Task<List<LeaveTypeDto>> GetAll()
		{
			var leaveTypes = await _mediator.Send(new GetLeaveTypesQuery());
			return leaveTypes;
		}

		// GET api/<LeaveTypesController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<LeaveTypeDetailsDto>> GetById(int id)
		{
			var leaveTypeDetails = await _mediator.Send(new GetLeaveTypeDetailsQuery(id));
			return Ok(leaveTypeDetails);
		}

		// POST api/<LeaveTypesController>
		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> Post(CreateLeaveTypeCommand createLeaveTypeCommand)
		{
			var response = await _mediator.Send(createLeaveTypeCommand);
			return NoContent();
		}

		// PUT api/<LeaveTypesController>/5
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(400)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Put(UpdateLeaveTypeCommand updateLeaveTypeCommand)
		{
			await _mediator.Send(updateLeaveTypeCommand);
			return NoContent();
		}

		// DELETE api/<LeaveTypesController>/5
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(400)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Delete(int id)
		{
			var command = new DeleteLeaveTypeCommand{ Id=id};
			await _mediator.Send(command);
			return NoContent();
		}
	}
}
