using MediatR;
using VirtualOffice.Application.Commands.CalendarEventCommands;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.CalendarEventHandlers
{
    public class CreateCalendarEventHandler : IRequestHandler<CreateCalendarEvent>
    {
        private readonly ICalendarEventRepository _repository;
        private readonly IMediator _mediator;

        public CreateCalendarEventHandler(ICalendarEventRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task Handle(CreateCalendarEvent request, CancellationToken cancellationToken)
        {
            var (Title, EventDescription, AssignedEmployees, StartDate, EndDate) = request;
            Guid guid = Guid.NewGuid();

            CalendarEvent calEv = new CalendarEvent(guid, Title, EventDescription, AssignedEmployees, StartDate, EndDate);

            await _repository.AddAsync(calEv);

            foreach (var domainEvent in calEv.Events)
                await _mediator.Publish(domainEvent, cancellationToken);
            calEv.ClearEvents();
        }
    }
}