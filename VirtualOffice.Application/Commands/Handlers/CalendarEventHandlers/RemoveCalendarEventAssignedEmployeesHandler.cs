using MediatR;
using VirtualOffice.Application.Commands.CalendarEventCommands;
using VirtualOffice.Application.Exceptions.CalendarEvent;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.Repositories;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.CalendarEventHandlers
{
    public class RemoveCalendarEventAssignedEmployeesHandler : IRequestHandler<RemoveCalendarEventAssignedEmployees>
    {
        private readonly ICalendarEventRepository _repository;
        private readonly ICalendarEventReadService _readService;
        private readonly IUserReadService _userReadService;
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;

        public RemoveCalendarEventAssignedEmployeesHandler(ICalendarEventRepository repository, ICalendarEventReadService readService,
            IMediator mediator, IUserReadService userReadService, IUserRepository userRepository)
        {
            _repository = repository;
            _readService = readService;
            _mediator = mediator;
            _userReadService = userReadService;
            _userRepository = userRepository;
        }

        public async Task Handle(RemoveCalendarEventAssignedEmployees request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.CalendarId))
                throw new CalendarEventDoesNotExistException(request.CalendarId);
            HashSet<ApplicationUser> employees = new();

            // retrive all employees by Id and assign them to employees
            foreach (var userId in request.EmployeesToRemove)
            {
                if (!await _userReadService.ExistsByIdAsync(userId))
                    throw new EmployeeNotFoundException(userId);
                employees.Add(await _userRepository.GetByIdAsync(userId));
            }

            var calEv = await _repository.GetByIdAsync(request.CalendarId);
            calEv.RemoveEmployeesRange(employees);
            await _repository.UpdateAsync(calEv);

            foreach (var domainEvent in calEv.Events)
                await _mediator.Publish(domainEvent, cancellationToken);
            calEv.ClearEvents();
        }
    }
}