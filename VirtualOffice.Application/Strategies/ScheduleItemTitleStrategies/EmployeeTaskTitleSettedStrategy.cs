using VirtualOffice.Application.IntegrationEvents.EmployeeTaskIntegrationEvents;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Domain.DomainEvents.ScheduleItemEvents;

namespace VirtualOffice.Application.Strategies.ScheduleItemTitleStrategies
{
    public class EmployeeTaskTitleSettedStrategy : IScheduleItemTitleSettedStrategy
    {
        public IOutboxMessageRepository _outboxMessageRepository { get; set; }

        public EmployeeTaskTitleSettedStrategy(IOutboxMessageRepository outboxMessageRepository)
        {
            _outboxMessageRepository = outboxMessageRepository;
        }

        public async Task Handle(ScheduleItemTitleSetted notification, CancellationToken cancellationToken)
        {
            EmployeeTaskTitleUpdatedIntegrationEvent integrationEvent = new EmployeeTaskTitleUpdatedIntegrationEvent
            {
                Id = notification.abstractScheduleItem.Id.Value.ToString(),
                Title = notification.title,
            };
            await _outboxMessageRepository.AddOutboxMessageAsync(integrationEvent);
        }
    }
}