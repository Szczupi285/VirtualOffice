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

        [HttpPatch("Update/{id}/Title")]
        public async Task UpdateCalendarEventTitle(Guid id, string title)
        {
            var command = new UpdateCalendarEventTitle(id, title);
            await _mediator.Send(command);
            Ok();
        }

        [HttpPatch("Update/{id}/Description")]
        public async Task UpdateCalendarEventDescription(Guid id, string description)
        {
            var command = new UpdateCalendarEventDescription(id, description);
            await _mediator.Send(command);
            Ok();
        }

        [HttpPatch("Update/{id}/Schedule")]
        public async Task UpdateCalendarEventSchedule(Guid id, DateTime startDate, DateTime endDate)
        {
            var command = new RescheduleCalendarEvent(id, startDate, endDate);
            await _mediator.Send(command);
            Ok();
        }

        [HttpDelete("Delete/CalendarEvent/{id}")]
        public async Task DeleteCalendarEvent(Guid id)
        {
            var command = new DeleteCalendarEvent(id);
            await _mediator.Send(command);
            Ok();
        }
    }
}