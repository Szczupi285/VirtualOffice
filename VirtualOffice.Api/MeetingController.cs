using MediatR;
using Microsoft.AspNetCore.Mvc;
using VirtualOffice.Application.Commands.MeetingCommands;
using VirtualOffice.Application.DTO.Meeting;
using VirtualOffice.Application.Models;
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
            Ok();
        }

        [HttpPatch("{Id}/title")]
        public async Task UpdateMeetingTitle(Guid Id, string newTitle)
        {
            var command = new UpdateMeetingTitle(Id, newTitle);
            await _mediator.Send(command);
            Ok();
        }

        [HttpPatch("{Id}/description")]
        public async Task UpdateMeetingDescription(Guid Id, string newDescription)
        {
            var command = new UpdateMeetingDescription(Id, newDescription);
            await _mediator.Send(command);
            Ok();
        }

        [HttpPatch("{Id}/schedule")]
        public async Task MeetingRescheduled(Guid Id, DateTime startDate, DateTime endDate)
        {
            var command = new RescheduleMeeting(Id, startDate, endDate);
            await _mediator.Send(command);
            Ok();
        }

        [HttpPost("{Id}/employees")]
        public async Task AddMeetingAssignedEmployees(Guid Id, HashSet<Guid> employeesToAdd)
        {
            var command = new AddAssignedEmployeesToMeeting(Id, employeesToAdd);
            await _mediator.Send(command);
            Ok();
        }

        [HttpDelete("{Id}/employees")]
        public async Task RemoveMeetingAssignedEmployees(Guid Id, HashSet<Guid> employeesToAdd)
        {
            var command = new RemoveAssignedEmployeesFromMeeting(Id, employeesToAdd);
            await _mediator.Send(command);
            Ok();
        }

        [HttpGet("{Id}")]
        public ActionResult<MeetingReadModel> GetMeetingById(Guid Id)
        {
            var meet = new MeetingReadModel
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
            return Ok(meet);
        }

        [HttpGet("/user/{userId}")]
        public ActionResult<MeetingTitleDTO> GetMeetingsForUser(Guid userId)
        {
            var mock = new MeetingTitleDTO
            {
                Id = Guid.NewGuid(),
                _Title = "mock1"
            };
            return Ok(mock);
        }

        [HttpGet("/user/{userId}/by-date")]
        public ActionResult<MeetingTitleDTO> GetMeetingsForUserByDate(Guid userId, DateTime startDate, DateTime endDate)
        {
            var mock = new MeetingTitleDTO
            {
                Id = Guid.NewGuid(),
                _Title = "mock1"
            };
            return Ok(mock);
        }

        [HttpGet("/user/{userId}/future")]
        public ActionResult<MeetingTitleDTO> GetFutureMeetingsForUser(Guid userId)
        {
            var mock = new MeetingTitleDTO
            {
                Id = Guid.NewGuid(),
                _Title = "mock1"
            };
            return Ok(mock);
        }
    }
}