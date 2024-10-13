using MediatR;
using VirtualOffice.Application.Commands.EmployeeTaskCommands;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.EmployeeTaskHandlers
{
    public class CreateEmployeeTaskHandler : IRequestHandler<CreateEmployeeTask>
    {
        private readonly IEmployeeTaskRepository _repository;
        private readonly IMediator _mediator;

        public CreateEmployeeTaskHandler(IEmployeeTaskRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task Handle(CreateEmployeeTask request, CancellationToken cancellationToken)
        {
            var (title, eventDescription, assignedEmployees, startDate, endDate, priority) = request;

            EmployeeTask empTask = new EmployeeTask(Guid.NewGuid(), title, eventDescription, assignedEmployees, priority, startDate, endDate);

            await _repository.AddAsync(empTask);

            foreach (var domainEvent in empTask.Events)
                await _mediator.Publish(domainEvent, cancellationToken);
            empTask.ClearEvents();
        }
    }
}