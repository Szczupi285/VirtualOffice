using AutoMapper;
using MediatR;
using VirtualOffice.Application.IntegrationEvents;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;
using VirtualOffice.Domain.DomainEvents.CalendarEventEvents;

namespace VirtualOffice.Application.Events
{
    public class CalendarEventStartDateUpadatedDomainEventHandler
        : INotificationHandler<CalendarEventStartDateUpdated>
    {
        private readonly IEventBus _eventBus;

        private readonly IMapper _mapper;

        public CalendarEventStartDateUpadatedDomainEventHandler(IEventBus eventBus, IMapper mapper)
        {
            _eventBus = eventBus;
            _mapper = mapper;
        }

        public async Task Handle(CalendarEventStartDateUpdated notification, CancellationToken cancellationToken)
        {
            await _eventBus.PublishAsync(new CalendarEventStartDateUpdatedIntegrationEvent
            {
                Id = notification.calendarEvent.Id.ToString(),
                Title = notification.calendarEvent._Title,
                Description = notification.calendarEvent._Description,
                AssignedEmployees = _mapper.Map<List<EmployeeReadModel>>(notification.calendarEvent._AssignedEmployees),
                StartDate = notification.calendarEvent._StartDate,
                EndDate = notification.calendarEvent._EndDate,
            }
            , cancellationToken);
        }
    }
}