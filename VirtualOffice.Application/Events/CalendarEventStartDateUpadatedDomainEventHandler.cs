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
                Id = notification.Id.ToString(),
                Title = notification.Title,
                Description = notification.Description,
                AssignedEmployees = _mapper.Map<List<EmployeeReadModel>>(notification.AssignedEmployees),
                StartDate = notification.StartDate,
                EndDate = notification.EndDate,
            }
            , cancellationToken);
        }
    }
}