using VirtualOffice.Application.IntegrationEvents.CalendarEventIntegrationEvents;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Domain.DomainEvents.ScheduleItemEvents;

namespace VirtualOffice.Application.Strategies.ScheduleItemRescheduledStrategies
{
    internal class CalendarEventRescheduledStrategy : IScheduleItemRescheduledStrategy
    {
        private readonly IOutboxMessageRepository _outboxMessageRepository;

        public CalendarEventRescheduledStrategy(IOutboxMessageRepository outboxMessageRepository)
        {
            _outboxMessageRepository = outboxMessageRepository;
        }

        public async Task Handle(ScheduleItemRescheduled notification, CancellationToken cancellationToken)
        {
            CalendarEventRescheduledIntegrationEvent integrationEvent = new()
            {
                Id = notification.AbstractScheduleItem.Id.Value.ToString(),
                StartDate = notification.StartDate,
                EndDate = notification.EndDate
            };
            await _outboxMessageRepository.AddOutboxMessageAsync(integrationEvent, cancellationToken);
        }
    }
}