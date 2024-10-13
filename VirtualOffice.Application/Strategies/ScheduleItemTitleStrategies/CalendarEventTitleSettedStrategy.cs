using AutoMapper;
using VirtualOffice.Application.IntegrationEvents.CalendarEventIntegrationEvents;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Domain.DomainEvents.ScheduleItemEvents;

namespace VirtualOffice.Application.Strategies.ScheduleItemTitleStrategies
{
    public class CalendarEventTitleSettedStrategy : IScheduleItemTitleSettedStrategy
    {
        private readonly IMapper _mapper;
        private readonly IOutboxMessageRepository _outboxMessageRepository;

        public CalendarEventTitleSettedStrategy(IMapper mapper, IOutboxMessageRepository outboxMessageRepository)
        {
            _mapper = mapper;
            _outboxMessageRepository = outboxMessageRepository;
        }

        public async Task Handle(ScheduleItemTitleSetted notification, CancellationToken cancellationToken)
        {
            CalendarEventTitleUpdatedIntegrationEvent integrationEvent = new CalendarEventTitleUpdatedIntegrationEvent
            {
                Id = notification.abstractScheduleItem.Id.Value.ToString(),
                Title = notification.abstractScheduleItem._Title,
            };
            await _outboxMessageRepository.AddOutboxMessageAsync(integrationEvent);
        }
    }
}