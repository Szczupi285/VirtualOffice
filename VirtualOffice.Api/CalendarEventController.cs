using MediatR;
using Microsoft.AspNetCore.Mvc;
using VirtualOffice.Application.Commands.CalendarEventCommands;
using VirtualOffice.Application.DTO.CalendarEvent;
using VirtualOffice.Application.Models;
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

        [HttpPatch("{id}/title")]
        public async Task UpdateCalendarEventTitle(Guid id, string title)
        {
            var command = new UpdateCalendarEventTitle(id, title);
            await _mediator.Send(command);
            Ok();
        }

        [HttpPatch("{id}/description")]
        public async Task UpdateCalendarEventDescription(Guid id, string description)
        {
            var command = new UpdateCalendarEventDescription(id, description);
            await _mediator.Send(command);
            Ok();
        }

        [HttpPatch("{id}/schedule")]
        public async Task UpdateCalendarEventSchedule(Guid id, DateTime startDate, DateTime endDate)
        {
            var command = new RescheduleCalendarEvent(id, startDate, endDate);
            await _mediator.Send(command);
            Ok();
        }

        [HttpDelete("{id}")]
        public async Task DeleteCalendarEvent(Guid id)
        {
            var command = new DeleteCalendarEvent(id);
            await _mediator.Send(command);
            Ok();
        }

        [HttpPost("{Id}/employees")]
        public async Task AddCalendarEventAssignedEmployees(Guid Id, HashSet<Guid> employeesToAdd)
        {
            var command = new AddCalendarEventAssignedEmployees(Id, employeesToAdd);
            await _mediator.Send(command);
            Ok();
        }

        [HttpDelete("{Id}/employees")]
        public async Task RemoveCalendarEventAssignedEmployees(Guid Id, HashSet<Guid> employeesToRemove)
        {
            var command = new RemoveCalendarEventAssignedEmployees(Id, employeesToRemove);
            await _mediator.Send(command);
            Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<CalendarEventReadModel> GetCalendarEventById(Guid id)
        {
            var calEv = new CalendarEventReadModel
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Team Meeting",
                Description = "Discuss project updates and timelines.",
                AssignedEmployees = new List<EmployeeReadModel>
                {
                    new EmployeeReadModel { Id = Guid.NewGuid().ToString(), Name = "Alice Johnson" },
                    new EmployeeReadModel { Id = Guid.NewGuid().ToString(), Name = "Bob Smith" }
                },
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(1).AddHours(1)
            };

            return Ok(calEv);
        }

        [HttpGet("/user/{id}")]
        public ActionResult<CalendarEventTitleDTO> GetCalendarEventForUser(Guid userId)
        {
            var list = new List<CalendarEventTitleDTO>()
            {
                new CalendarEventTitleDTO()
                {
                    Id = Guid.NewGuid(),
                    _Title = "mock1"
                },
                new CalendarEventTitleDTO()
                {
                    Id = Guid.NewGuid(),
                    _Title = "mock2"
                }
            };
            return Ok(list);
        }

        [HttpGet("/user/{id}/by-date")]
        public ActionResult<CalendarEventTitleDTO> GetCalendarEventForUser(Guid userId, DateTime startDate, DateTime endDate)
        {
            var list = new List<CalendarEventTitleDTO>()
            {
                new CalendarEventTitleDTO()
                {
                    Id = Guid.NewGuid(),
                    _Title = "mock1"
                },
                new CalendarEventTitleDTO()
                {
                    Id = Guid.NewGuid(),
                    _Title = "mock2"
                }
            };
            return Ok(list);
        }

        [HttpGet("/user/{id}/future")]
        public ActionResult<CalendarEventTitleDTO> GetFutureCalendarEventForUser(Guid userId)
        {
            var list = new List<CalendarEventTitleDTO>()
            {
                new CalendarEventTitleDTO()
                {
                    Id = Guid.NewGuid(),
                    _Title = "mock1"
                },
                new CalendarEventTitleDTO()
                {
                    Id = Guid.NewGuid(),
                    _Title = "mock2"
                }
            };
            return Ok(list);
        }
    }
}