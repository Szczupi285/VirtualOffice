using MediatR;
using VirtualOffice.Application.Commands.EmployeeTaskCommands;
using VirtualOffice.Application.Exceptions.EmployeeTask;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.EmployeeTaskHandlers
{
    public class UpdateEmployeeTaskHandler : IRequestHandler<UpdateEmployeeTask>
    {
        private readonly IEmployeeTaskRepository _repository;
        private readonly IEmployeeTaskReadService _readService;

        public UpdateEmployeeTaskHandler(IEmployeeTaskRepository repository, IEmployeeTaskReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(UpdateEmployeeTask request, CancellationToken cancellationToken)
        {
            var (id, title, description, endDate, status, priority) = request;

            if (!await _readService.ExistsByIdAsync(id))
                throw new EmployeeTaskDoesNotExistsException(id);

            var empTask = await _repository.GetByIdAsync(id);

            // we update only changed properties rather than whole object
            // beacuse changing the title to the same title would raise an event.
            if (empTask._Title != title)
                empTask.SetTitle(title);
            if (empTask._Description != description)
                empTask.SetDescription(description);
            if (empTask._EndDate != endDate)
                empTask.UpdateEndDate(endDate);
            if (empTask._TaskStatus != status)
                empTask.UpdateStatus(status);
            if (empTask._Priority != priority)
                empTask.SetPriority(priority);

            await _repository.UpdateAsync(empTask);
        }
    }
}