using MediatR;
using VirtualOffice.Application.Commands.EmployeeTaskCommands;
using VirtualOffice.Application.Exceptions.EmployeeTask;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.Repositories;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.EmployeeTaskHandlers
{
    internal sealed class AddAssignedEmployeesToEmployeeTaskHandler : IRequestHandler<AddAssignedEmployeesToEmployeeTask>
    {
        private readonly IEmployeeTaskRepository _repository;
        private readonly IEmployeeTaskReadService _readService;
        private readonly IUserRepository _userRepository;
        private readonly IUserReadService _userReadService;
        private readonly IMediator _mediator;

        public AddAssignedEmployeesToEmployeeTaskHandler(IEmployeeTaskRepository repository,
            IEmployeeTaskReadService readService, IMediator mediator,
            IUserRepository userRepository, IUserReadService userReadService)
        {
            _repository = repository;
            _readService = readService;
            _mediator = mediator;
            _userRepository = userRepository;
            _userReadService = userReadService;
        }

        public async Task Handle(AddAssignedEmployeesToEmployeeTask request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.Id, cancellationToken))
                throw new EmployeeTaskDoesNotExistsException(request.Id);

            HashSet<ApplicationUser> employees = new();

            foreach (var userId in request.EmployeesToAdd)
            {
                // check if employee with given id Exists if so, retrive it from db and add to hashset for future use
                if (!await _userReadService.ExistsByIdAsync(userId, cancellationToken))
                    throw new EmployeeNotFoundException(userId);
                employees.Add(await _userRepository.GetByIdAsync(userId, cancellationToken));
            }

            var empTask = await _repository.GetByIdAsync(request.Id, cancellationToken);
            empTask.AddEmployeesRange(employees);

            await _repository.UpdateAsync(empTask);

            foreach (var domainEvent in empTask.Events)
                await _mediator.Publish(domainEvent, cancellationToken);
            empTask.ClearEvents();
        }
    }
}