﻿using MediatR;
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

        [HttpPatch("{id}")]
        public async Task UpdateCalendarEventStartDate(Guid id, [FromBody] CalendarEventTitleDescDTO request)
        {
            var command = new UpdateCalendarEvent(id, request.Title, request.Description, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
            await _mediator.Send(command);
        }

        [HttpPatch("UpdateTitle/{id}")]
        public async Task UpdateCalendarEventTitle(Guid id, [FromBody] string title)
        {
            var command = new UpdateCalendarEventTitle(id, title);
            await _mediator.Send(command);
        }
    }
}