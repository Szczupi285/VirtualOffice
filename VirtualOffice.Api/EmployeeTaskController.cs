using MediatR;
using Microsoft.AspNetCore.Mvc;
using VirtualOffice.Application.Commands.EmployeeTaskCommands;
using VirtualOffice.Application.DTO.EmployeeTask;
using VirtualOffice.Application.Models;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Api
{
    [ApiController]
    [Route("api/EmployeeTasks")]
    public class EmployeeTaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeTaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task CreateEmployeeTask(EmployeeTaskDTO request)
        {
            var command = new CreateEmployeeTask(request._Title, request._Description, new HashSet<ApplicationUser>(), request._StartDate, request._EndDate, request._Priority);
            await _mediator.Send(command);
            Created();
        }

        [HttpDelete]
        public async Task DeleteEmployeeTask(Guid id)
        {
            var command = new DeleteEmployeeTask(id);
            await _mediator.Send(command);
            Ok();
        }

        [HttpPost("{Id}/employees")]
        public async Task AddEmployeeTaskAssignedEmployees(Guid Id, HashSet<Guid> employeesToAdd)
        {
            var command = new AddAssignedEmployeesToEmployeeTask(Id, employeesToAdd);
            await _mediator.Send(command);
            Ok();
        }

        [HttpDelete("{Id}/employees")]
        public async Task RemoveEmployeeTaskAssignedEmployees(Guid Id, HashSet<Guid> employeesToRemove)
        {
            var command = new RemoveAssignedEmployeesFromEmployeeTask(Id, employeesToRemove);
            await _mediator.Send(command);
            Ok();
        }

        [HttpPatch("{Id}/title")]
        public async Task EmployeeTaskUpdateTitle(Guid Id, string title)
        {
            var command = new UpdateEmployeeTaskTitle(Id, title);
            await _mediator.Send(command);
            Ok();
        }

        [HttpPatch("{Id}/description")]
        public async Task EmployeeTaskUpdatedDescription(Guid Id, string description)
        {
            var command = new UpdateEmployeeTaskDescription(Id, description);
            await _mediator.Send(command);
            Ok();
        }

        [HttpPatch("{Id}/schedule")]
        public async Task EmployeeTaskRescheduled(Guid Id, DateTime startDate, DateTime endDate)
        {
            var command = new RescheduleEmployeeTask(Id, startDate, endDate);
            await _mediator.Send(command);
            Ok();
        }

        [HttpPatch("{Id}/status")]
        public async Task EmployeeTaskStatusUpdated(Guid Id, EmployeeTaskStatusEnum status)
        {
            var command = new UpdateEmployeeTaskStatus(Id, status);
            await _mediator.Send(command);
            Ok();
        }

        [HttpPatch("{Id}/priority")]
        public async Task EmployeeTaskPriorityUpdated(Guid Id, EmployeeTaskPriorityEnum priority)
        {
            var command = new UpdateEmployeeTaskPriority(Id, priority);
            await _mediator.Send(command);
            Ok();
        }

        [HttpGet("{Id}")]
        public ActionResult<EmployeeTaskReadModel> GetEmployeeTaskById(Guid id)
        {
            var EmpTask = new EmployeeTaskReadModel
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

            return Ok(EmpTask);
        }

        [HttpGet("/user/{Id}")]
        public ActionResult<EmployeeTaskTitleDTO> GetEmployeeTaskForUser(Guid userId)
        {
            var list = new List<EmployeeTaskTitleDTO>()
            {
                 new EmployeeTaskTitleDTO
                 {
                    Id = Guid.NewGuid(),
                    _Title = "mock1"
                 },
                 new EmployeeTaskTitleDTO
                 {
                    Id = Guid.NewGuid(),
                    _Title = "mock2"
                 },
            };

            return Ok(list);
        }

        [HttpGet("/user/{Id}/by-date")]
        public ActionResult<EmployeeTaskTitleDTO> GetEmployeeTaskForUserByDate(Guid userId, DateTime startDate, DateTime endDate)
        {
            var list = new List<EmployeeTaskTitleDTO>()
            {
                 new EmployeeTaskTitleDTO
                 {
                    Id = Guid.NewGuid(),
                    _Title = "mock1"
                 },
                 new EmployeeTaskTitleDTO
                 {
                    Id = Guid.NewGuid(),
                    _Title = "mock2"
                 },
            };

            return Ok(list);
        }

        [HttpGet("/user/{Id}/by-priority")]
        public ActionResult<EmployeeTaskTitleDTO> GetEmployeeTaskForUserByPriority(Guid userId, EmployeeTaskPriorityEnum priority)
        {
            var list = new List<EmployeeTaskTitleDTO>()
            {
                 new EmployeeTaskTitleDTO
                 {
                    Id = Guid.NewGuid(),
                    _Title = "mock1"
                 },
                 new EmployeeTaskTitleDTO
                 {
                    Id = Guid.NewGuid(),
                    _Title = "mock2"
                 },
            };

            return Ok(list);
        }

        [HttpGet("/user/{Id}/by-priority-and-date")]
        public ActionResult<EmployeeTaskTitleDTO> GetEmployeeTaskForUserByPriorityAndDate(Guid userId,
            EmployeeTaskPriorityEnum priority, DateTime startDate, DateTime endDate)
        {
            var list = new List<EmployeeTaskTitleDTO>()
            {
                 new EmployeeTaskTitleDTO
                 {
                    Id = Guid.NewGuid(),
                    _Title = "mock1"
                 },
                 new EmployeeTaskTitleDTO
                 {
                    Id = Guid.NewGuid(),
                    _Title = "mock2"
                 },
            };

            return Ok(list);
        }

        [HttpGet("/user/{Id}/by-status")]
        public ActionResult<EmployeeTaskTitleDTO> GetEmployeeTaskForUserByStatusAndDate(Guid userId,
            EmployeeTaskStatusEnum status)
        {
            var list = new List<EmployeeTaskTitleDTO>()
            {
                 new EmployeeTaskTitleDTO
                 {
                    Id = Guid.NewGuid(),
                    _Title = "mock1"
                 },
                 new EmployeeTaskTitleDTO
                 {
                    Id = Guid.NewGuid(),
                    _Title = "mock2"
                 },
            };

            return Ok(list);
        }

        [HttpGet("/user/{Id}/by-status-and-date")]
        public ActionResult<EmployeeTaskTitleDTO> GetEmployeeTaskForUserByStatusAndDate(Guid userId,
         EmployeeTaskStatusEnum status, DateTime startDate, DateTime endDate)
        {
            var list = new List<EmployeeTaskTitleDTO>()
            {
                 new EmployeeTaskTitleDTO
                 {
                    Id = Guid.NewGuid(),
                    _Title = "mock1"
                 },
                 new EmployeeTaskTitleDTO
                 {
                    Id = Guid.NewGuid(),
                    _Title = "mock2"
                 },
            };

            return Ok(list);
        }

        [HttpGet("/user/{Id}/future")]
        public ActionResult<EmployeeTaskTitleDTO> GetFutureEmployeeTaskForUser(Guid userId)
        {
            var list = new List<EmployeeTaskTitleDTO>()
            {
                 new EmployeeTaskTitleDTO
                 {
                    Id = Guid.NewGuid(),
                    _Title = "mock1"
                 },
                 new EmployeeTaskTitleDTO
                 {
                    Id = Guid.NewGuid(),
                    _Title = "mock2"
                 },
            };

            return Ok(list);
        }
    }
}