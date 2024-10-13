using MediatR;
using Microsoft.AspNetCore.Mvc;
using VirtualOffice.Application.Commands.EmployeeTaskCommands;
using VirtualOffice.Application.DTO.EmployeeTask;
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
    }
}