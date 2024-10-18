using VirtualOffice.Application.IntegrationEvents.EmployeeTaskIntegrationEvents;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Domain.DomainEvents.ScheduleItemEvents;

namespace VirtualOffice.Application.Strategies.ScheduleItemDescriptionStrategies
{
    internal class EmployeeTaskDescriptionSetterStrategy : IScheduleItemDescriptionSettedStrategy
    {
        private readonly IOutboxMessageRepository _outboxMessageRepository;

        public EmployeeTaskDescriptionSetterStrategy(IOutboxMessageRepository outboxMessageRepository)
        {
            _outboxMessageRepository = outboxMessageRepository;
        }

        public async Task Handle(ScheduleItemDescriptionSetted notification, CancellationToken cancellationToken)
        {
            EmployeeTaskDescriptionUpdatedIntegrationEvent integrationEvent = new()
            {
                Id = notification.abstractScheduleItem.Id.Value.ToString(),
                Description = notification.description,
            };

            await _outboxMessageRepository.AddOutboxMessageAsync(integrationEvent);
        }
    }
}