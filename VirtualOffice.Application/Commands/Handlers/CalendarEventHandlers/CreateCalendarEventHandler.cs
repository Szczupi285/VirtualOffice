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
        private readonly ICalendarEventReadService _readService;

        public CreateCalendarEventHandler(ICalendarEventRepository repository, ICalendarEventReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(CreateCalendarEvent request, CancellationToken cancellationToken)
        {
            var (Id, Title, EventDescription, AssignedEmployees, StartDate, EndDate) = request;

            if (await _readService.ExistsByIdAsync(Id))
            {
                throw new CalendarEventAlreadyExistsException(Id);
            }
            CalendarEvent calEv = new CalendarEvent(Id, Title, EventDescription, AssignedEmployees, StartDate, EndDate);

            await _repository.Add(calEv);
        }

      
    }
}
