using MediatR;
using VirtualOffice.Application.Commands.MeetingCommands;
using VirtualOffice.Application.Exceptions.CalendarEvent;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.Repositories;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.MeetingHandlers
{
    internal sealed class RemoveAssignedEmployeesFromMeetingHandler : IRequestHandler<RemoveAssignedEmployeesFromMeeting>
    {
        private readonly IMeetingRepository _repository;
        private readonly IMeetingReadService _readService;
        private readonly IUserRepository _userRepository;
        private readonly IUserReadService _userReadService;
        private readonly IMediator _mediator;

        public RemoveAssignedEmployeesFromMeetingHandler(IMeetingRepository repository, IMeetingReadService readService,
            IMediator mediator, IUserRepository userRepository, IUserReadService userReadService)
        {
            _repository = repository;
            _readService = readService;
            _mediator = mediator;
            _userRepository = userRepository;
            _userReadService = userReadService;
        }

        public async Task Handle(RemoveAssignedEmployeesFromMeeting request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.Id, cancellationToken))
                throw new CalendarEventDoesNotExistException(request.Id);
            HashSet<ApplicationUser> employees = new();

            // retrive all employees by Id and assign them to employees
            foreach (var userId in request.EmployeesToRemove)
            {
                if (!await _userReadService.ExistsByIdAsync(userId, cancellationToken))
                    throw new EmployeeNotFoundException(userId);
                employees.Add(await _userRepository.GetByIdAsync(userId, cancellationToken));
            }

            var meeting = await _repository.GetByIdAsync(request.Id, cancellationToken);
            meeting.RemoveEmployeesRange(employees);
            await _repository.UpdateAsync(meeting, cancellationToken);

            foreach (var domainEvent in meeting.Events)
                await _mediator.Publish(domainEvent, cancellationToken);
            meeting.ClearEvents();
        }
    }
}