using MediatR;
using VirtualOffice.Application.Commands.MeetingCommands;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.MeetingEventHandlers
{
    internal sealed class CreateMeetingHandler : IRequestHandler<CreateMeeting>
    {
        private readonly IMeetingRepository _repository;
        private readonly IMediator _mediator;

        public CreateMeetingHandler(IMeetingRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task Handle(CreateMeeting request, CancellationToken cancellationToken)
        {
            var (Title, Description, AssignedEmployees, StartDate, EndDate) = request;

            Meeting meeting = new Meeting(Guid.NewGuid(), Title, Description, AssignedEmployees, StartDate, EndDate);

            await _repository.AddAsync(meeting);

            foreach (var domainEvent in meeting.Events)
                await _mediator.Publish(domainEvent, cancellationToken);
            meeting.ClearEvents();
        }
    }
}