using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hr.LeaveManagement.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LeaveRequestController : ControllerBase
	{
		private readonly IMediator _mediator;

		public LeaveRequestController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<ActionResult<List<LeaveRequestListDto>>> GetAll(bool isLoggedInUser = false)
		{
			var leaveRequest = _mediator.Send(new GetAllLeaveRequestQuery());
			return Ok(leaveRequest);
		}

		[HttpGet("id")]
		public async Task<ActionResult<LeaveRequestDetailsDto>> Get(int id)
		{
			var leaveRequestDetails= _mediator.Send(new GetLeaveRequestDetailQuery { Id=id});
			return Ok(leaveRequestDetails);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(400)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Post(CreateLeaveRequestCommand command)
		{
			var response = _mediator.Send(command);
			return CreatedAtAction(nameof(Get), new { id = response });
		}
		[HttpPut("id")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(400)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Put(UpdateLeaveRequestCommand command)
		{
			await _mediator.Send(command);
			return NoContent();
		}
		[HttpDelete("id")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(400)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Delete(int id)
		{
			var command = new DeleteLeaveRequestCommand { Id = id };
			await _mediator.Send(command);
			return NoContent();
		}
		//Extras other than crud
		[HttpPut]
		[Route("CancelRequest")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(400)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> CancelRequest(CancelLeaveRequestCommand command)
		{
			await _mediator.Send(command);
			return NoContent();
		}
		[HttpPut]
		[Route("UpdateApproval")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(400)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> UpdateApproval(UpdateLeaveRequestCommand command)
		{
			await _mediator.Send(command);
			return NoContent();
		}

	}
}
