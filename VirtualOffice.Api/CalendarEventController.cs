using MediatR;
using Microsoft.AspNetCore.Mvc;
using VirtualOffice.Application.Commands.CalendarEventCommands;
using VirtualOffice.Application.DTO.CalendarEvent;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Api
{
    [ApiController]
    [Route("api/CalendarEvents")]
    public class CalendarEventController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CalendarEventController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task CreateCalendarEvent([FromBody] CalendarEventDTO request)
        {
            var command = new CreateCalendarEvent(request._Title, request._Description, new HashSet<ApplicationUser>(), request._StartDate, request._EndDate);
            await _mediator.Send(command);
            Created();
        }

        [HttpPatch]
        public async Task UpdateCalendarEventTitle([FromBody] UpdateCalendarEventTitle request)
        {
            var command = new UpdateCalendarEventTitle(request.Id, request.Title);
            await _mediator.Send(command);
            Ok();
        }
    }
}