using VirtualOffice.Application.IntegrationEvents.EmployeeTaskIntegrationEvents;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Domain.DomainEvents.ScheduleItemEvents;

namespace VirtualOffice.Application.Strategies.ScheduleItemRescheduledStrategies
{
    internal class EmployeeTaskRescheduledStrategy : IScheduleItemRescheduledStrategy
    {
        private readonly IOutboxMessageRepository _outboxMessageRepository;

        public EmployeeTaskRescheduledStrategy(IOutboxMessageRepository outboxMessageRepository)
        {
            _outboxMessageRepository = outboxMessageRepository;
        }

        public async Task Handle(ScheduleItemRescheduled notification, CancellationToken cancellationToken)
        {
            EmployeeTaskRescheduledIntegrationEvent integrationEvent = new()
            {
                Id = notification.AbstractScheduleItem.Id.Value.ToString(),
                StartDate = notification.StartDate,
                EndDate = notification.EndDate,
            };

            await _outboxMessageRepository.AddOutboxMessageAsync(integrationEvent, cancellationToken);
        }
    }
}