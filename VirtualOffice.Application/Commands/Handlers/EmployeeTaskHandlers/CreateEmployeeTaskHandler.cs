using MediatR;
using VirtualOffice.Application.Commands.EmployeeTaskCommands;
using VirtualOffice.Application.Exceptions.EmployeeTask;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.EmployeeTaskHandlers
{
    public class CreateEmployeeTaskHandler : IRequestHandler<CreateEmployeeTask>
    {
        private readonly IEmployeeTaskRepository _repository;
        private readonly IEmployeeTaskReadService _readService;

        public CreateEmployeeTaskHandler(IEmployeeTaskRepository repository, IEmployeeTaskReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(CreateEmployeeTask request, CancellationToken cancellationToken)
        {

            var (id, title, eventDescription, assignedEmployees, startDate, endDate, priority) = request;

            if (await _readService.ExistsByIdAsync(id))
            {
                throw new EmployeeTaskAlreadyExistsException(id);
            }
            EmployeeTask empTask = new EmployeeTask(id, title, eventDescription, assignedEmployees, priority, startDate, endDate);

            await _repository.Add(empTask);
            await _repository.SaveAsync(cancellationToken);

        }
    }
}
