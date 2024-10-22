using MediatR;
using VirtualOffice.Application.Commands.MeetingCommands;
using VirtualOffice.Application.Exceptions.Meeting;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.Repositories;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.MeetingHandlers
{
    public class AddAssignedEmployeeToMeetingHandler : IRequestHandler<AddAssignedEmployeesToMeeting>
    {
        private readonly IMeetingRepository _repository;
        private readonly IMeetingReadService _readService;
        private readonly IUserRepository _userRepository;
        private readonly IUserReadService _userReadService;
        private readonly IMediator _mediator;

        public AddAssignedEmployeeToMeetingHandler(IMeetingRepository repository, IMeetingReadService readService,
            IMediator mediator, IUserRepository userRepository, IUserReadService userReadService)
        {
            _repository = repository;
            _readService = readService;
            _mediator = mediator;
            _userRepository = userRepository;
            _userReadService = userReadService;
        }

        public async Task Handle(AddAssignedEmployeesToMeeting request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.Id, cancellationToken))
                throw new MeetingDoesNotExistException(request.Id);
            HashSet<ApplicationUser> employees = new();
            foreach (var userId in request.EmployeesToAdd)
            {
                // check if employee with given id Exists if so, retrive it from db and add to hashset for future use
                if (!await _userReadService.ExistsByIdAsync(userId, cancellationToken))
                    throw new EmployeeNotFoundException(userId);
                employees.Add(await _userRepository.GetByIdAsync(userId, cancellationToken));
            }

            var meeting = await _repository.GetByIdAsync(request.Id, cancellationToken);
            meeting.AddEmployeesRange(employees);
            await _repository.UpdateAsync(meeting, cancellationToken);

            foreach (var domainEvent in meeting.Events)
                await _mediator.Publish(domainEvent, cancellationToken);
            meeting.ClearEvents();
        }
    }
}