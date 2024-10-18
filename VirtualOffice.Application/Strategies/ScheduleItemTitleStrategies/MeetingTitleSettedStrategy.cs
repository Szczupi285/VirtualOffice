using VirtualOffice.Application.IntegrationEvents.MeetingIntegrationEvents;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Domain.DomainEvents.ScheduleItemEvents;

namespace VirtualOffice.Application.Strategies.ScheduleItemTitleStrategies
{
    internal class MeetingTitleSettedStrategy : IScheduleItemTitleSettedStrategy
    {
        private readonly IOutboxMessageRepository _outboxMessageRepository;

        public MeetingTitleSettedStrategy(IOutboxMessageRepository outboxMessageRepository)
        {
            _outboxMessageRepository = outboxMessageRepository;
        }

        public async Task Handle(ScheduleItemTitleSetted notification, CancellationToken cancellationToken)
        {
            MeetingTitleUpdatedIntegrationEvent integrationEvent = new()
            {
                Id = notification.abstractScheduleItem.Id.Value.ToString(),
                Title = notification.abstractScheduleItem._Title,
            };
            await _outboxMessageRepository.AddOutboxMessageAsync(integrationEvent);
        }
    }
}