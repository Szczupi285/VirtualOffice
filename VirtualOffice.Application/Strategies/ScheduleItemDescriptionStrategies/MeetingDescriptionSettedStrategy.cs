using VirtualOffice.Application.IntegrationEvents.MeetingIntegrationEvents;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Domain.DomainEvents.ScheduleItemEvents;

namespace VirtualOffice.Application.Strategies.ScheduleItemDescriptionStrategies
{
    internal class MeetingDescriptionSettedStrategy : IScheduleItemDescriptionSettedStrategy
    {
        private readonly IOutboxMessageRepository _outboxMessageRepository;

        public MeetingDescriptionSettedStrategy(IOutboxMessageRepository outboxMessageRepository)
        {
            _outboxMessageRepository = outboxMessageRepository;
        }

        public async Task Handle(ScheduleItemDescriptionSetted notification, CancellationToken cancellationToken)
        {
            MeetingDescriptionUpdatedIntegrationEvent integrationEvent = new()
            {
                Id = notification.abstractScheduleItem.Id.Value.ToString(),
                Description = notification.description
            };
            await _outboxMessageRepository.AddOutboxMessageAsync(integrationEvent);
        }
    }
}