using MediatR;
using VirtualOffice.Application.Commands.CalendarEventCommands;
using VirtualOffice.Application.Exceptions.CalendarEvent;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.Repositories;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.CalendarEventHandlers
{
    public class AddCalendarEventAssignedEmployeesHandler : IRequestHandler<AddCalendarEventAssignedEmployees>
    {
        private readonly ICalendarEventRepository _repository;
        private readonly ICalendarEventReadService _readService;
        private readonly IUserRepository _userRepository;
        private readonly IUserReadService _userReadService;
        private readonly IMediator _mediator;

        public AddCalendarEventAssignedEmployeesHandler(ICalendarEventRepository repository, ICalendarEventReadService readService,
            IMediator mediator, IUserRepository userRepository, IUserReadService userReadService)
        {
            _repository = repository;
            _readService = readService;
            _mediator = mediator;
            _userRepository = userRepository;
            _userReadService = userReadService;
        }

        public async Task Handle(AddCalendarEventAssignedEmployees request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.CalendarId))
                throw new CalendarEventDoesNotExistException(request.CalendarId);
            HashSet<ApplicationUser> employees = new();
            foreach (var userId in request.EmployeesToAdd)
            {
                // check if employee with given id Exists if so, retrive it from db and add to hashset for future use
                if (!await _userReadService.ExistsByIdAsync(userId))
                    throw new EmployeeNotFoundException(userId);
                employees.Add(await _userRepository.GetByIdAsync(userId));
            }

            var calEv = await _repository.GetByIdAsync(request.CalendarId);
            calEv.AddEmployeesRange(employees);
            await _repository.UpdateAsync(calEv);

            foreach (var domainEvent in calEv.Events)
                await _mediator.Publish(domainEvent, cancellationToken);
            calEv.ClearEvents();
        }
    }
}