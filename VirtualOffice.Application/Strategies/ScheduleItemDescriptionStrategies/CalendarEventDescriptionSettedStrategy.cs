using AutoMapper;
using VirtualOffice.Application.IntegrationEvents.CalendarEventIntegrationEvents;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Domain.DomainEvents.ScheduleItemEvents;

namespace VirtualOffice.Application.Strategies.ScheduleItemDescriptionStrategies
{
    public class CalendarEventDescriptionSettedStrategy : IScheduleItemDescriptionSettedStrategy
    {
        private readonly IMapper _mapper;
        private readonly IOutboxMessageRepository _outboxMessageRepository;

        public CalendarEventDescriptionSettedStrategy(IMapper mapper, IOutboxMessageRepository outboxMessageRepository)
        {
            _mapper = mapper;
            _outboxMessageRepository = outboxMessageRepository;
        }

        public async Task Handle(ScheduleItemDescriptionSetted notification, CancellationToken cancellationToken)
        {
            CalendarEventDescriptionUpdatedIntegrationEvent integrationEvent = new CalendarEventDescriptionUpdatedIntegrationEvent
            {
                Id = notification.abstractScheduleItem.Id.Value.ToString(),
                Description = notification.abstractScheduleItem._Description,
            };

            await _outboxMessageRepository.AddOutboxMessageAsync(integrationEvent);
        }
    }
}