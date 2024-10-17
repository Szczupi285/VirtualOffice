using MediatR;
using Microsoft.AspNetCore.Mvc;
using VirtualOffice.Application.Commands.MeetingCommands;
using VirtualOffice.Application.DTO.Meeting;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Api
{
    [ApiController]
    [Route("api/Meetings")]
    public class MeetingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MeetingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task CreateMeeting([FromBody] MeetingDTO request)
        {
            var command = new CreateMeeting(request._Title, request._Description, new HashSet<ApplicationUser>(), request._StartDate, request._EndDate);
            await _mediator.Send(command);
            Created();
        }

        [HttpDelete]
        public async Task DeleteMeeting(Guid id)
        {
            var command = new DeleteMeeting(id);
            await _mediator.Send(command);
            Created();
        }
    }
}