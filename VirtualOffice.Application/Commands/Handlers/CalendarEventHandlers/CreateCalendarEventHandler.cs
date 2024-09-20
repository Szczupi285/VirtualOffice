using MediatR;
using VirtualOffice.Application.Commands.CalendarEventCommands;
using VirtualOffice.Application.Exceptions.CalendarEvent;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.CalendarEventHandlers
{
    public class CreateCalendarEventHandler : IRequestHandler<CreateCalendarEvent>
    {
        private readonly ICalendarEventRepository _repository;

        public CreateCalendarEventHandler(ICalendarEventRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateCalendarEvent request, CancellationToken cancellationToken)
        {
            var (Title, EventDescription, AssignedEmployees, StartDate, EndDate) = request;

            CalendarEvent calEv = new CalendarEvent(Guid.NewGuid(), Title, EventDescription, AssignedEmployees, StartDate, EndDate);

            await _repository.AddAsync(calEv);
        }
    }
}