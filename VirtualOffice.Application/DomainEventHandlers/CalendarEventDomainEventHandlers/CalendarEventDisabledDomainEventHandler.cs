using AutoMapper;
using MediatR;
using VirtualOffice.Application.IntegrationEvents.CalendarEventIntegrationEvents;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Domain.DomainEvents.CalendarEventEvents;

namespace VirtualOffice.Application.DomainEventHandlers.CalendarEventDomainEventHandlers
{
    public class CalendarEventDisabledDomainEventHandler : INotificationHandler<CalendarEventDisabled>
    {
        private readonly IEventBus _eventBus;
        private readonly IMapper _mapper;
        private readonly IOutboxMessageRepository _outboxMessageRepository;

        public CalendarEventDisabledDomainEventHandler(IEventBus eventBus, IMapper mapper, IOutboxMessageRepository outboxMessageRepository)
        {
            _eventBus = eventBus;
            _mapper = mapper;
            _outboxMessageRepository = outboxMessageRepository;
        }

        public async Task Handle(CalendarEventDisabled notification, CancellationToken cancellationToken)
        {
            CalendarEventDeletedIntegrationEvent integrationEvent = new()
            {
                Id = notification.Id,
            };
            await _outboxMessageRepository.AddOutboxMessageAsync(integrationEvent);
        }
    }
}