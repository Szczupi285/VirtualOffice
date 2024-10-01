using AutoMapper;
using MediatR;
using VirtualOffice.Application.IntegrationEvents;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Domain.DomainEvents.CalendarEventEvents;

namespace VirtualOffice.Application.Events
{
    public class CalendarEventTitleUpdatedDomainEventHandler : INotificationHandler<CalendarEventTitleUpdated>
    {
        private readonly IEventBus _eventBus;

        private readonly IMapper _mapper;

        public CalendarEventTitleUpdatedDomainEventHandler(IEventBus eventBus, IMapper mapper)
        {
            _eventBus = eventBus;
            _mapper = mapper;
        }

        public async Task Handle(CalendarEventTitleUpdated notification, CancellationToken cancellationToken)
        {
            await _eventBus.PublishAsync(new CalendarEventTitleUpdatedIntegrationEvent
            {
                Id = notification.Id.ToString(),
                Title = notification.Title,
            });
        }
    }
}