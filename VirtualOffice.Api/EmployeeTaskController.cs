using MediatR;
using Microsoft.AspNetCore.Mvc;
using VirtualOffice.Application.Commands.EmployeeTaskCommands;
using VirtualOffice.Application.DTO.EmployeeTask;
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
            Ok();
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
    }
}