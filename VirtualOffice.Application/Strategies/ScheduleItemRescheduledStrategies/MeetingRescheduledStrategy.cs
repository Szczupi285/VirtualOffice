using VirtualOffice.Application.IntegrationEvents.MeetingIntegrationEvents;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Domain.DomainEvents.ScheduleItemEvents;

namespace VirtualOffice.Application.Strategies.ScheduleItemRescheduledStrategies
{
    internal sealed class MeetingRescheduledStrategy : IScheduleItemRescheduledStrategy
    {
        private readonly IOutboxMessageRepository _outboxMessageRepository;

        public MeetingRescheduledStrategy(IOutboxMessageRepository outboxMessageRepository)
        {
            _outboxMessageRepository = outboxMessageRepository;
        }

        public async Task Handle(ScheduleItemRescheduled notification, CancellationToken cancellationToken)
        {
            MeetingScheduleUpdatedIntegrationEvent integrationEvent = new()
            {
                Id = notification.AbstractScheduleItem.Id.Value.ToString(),
                StartDate = notification.StartDate,
                EndDate = notification.EndDate,
            };
            await _outboxMessageRepository.AddOutboxMessageAsync(integrationEvent);
        }
    }
}