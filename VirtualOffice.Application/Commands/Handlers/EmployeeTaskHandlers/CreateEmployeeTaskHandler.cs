using MediatR;
using VirtualOffice.Application.Commands.EmployeeTaskCommands;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.EmployeeTaskHandlers
{
    public class CreateEmployeeTaskHandler : IRequestHandler<CreateEmployeeTask>
    {
        private readonly IEmployeeTaskRepository _repository;

        public CreateEmployeeTaskHandler(IEmployeeTaskRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateEmployeeTask request, CancellationToken cancellationToken)
        {
            var (title, eventDescription, assignedEmployees, startDate, endDate, priority) = request;

            EmployeeTask empTask = new EmployeeTask(Guid.NewGuid(), title, eventDescription, assignedEmployees, priority, startDate, endDate);

            await _repository.AddAsync(empTask);
        }
    }
}