using MediatR;
using VirtualOffice.Application.IntegrationEvents.EmployeeTaskIntegrationEvents;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Domain.DomainEvents.EmployeeTask;

namespace VirtualOffice.Application.DomainEventHandlers.EmployeeTaskDomainEventHandlers
{
    internal sealed class PrioritySettedDomainEventHandler : INotificationHandler<PrioritySetted>
    {
        private readonly IOutboxMessageRepository _outboxMessageRepository;

        public PrioritySettedDomainEventHandler(IOutboxMessageRepository outboxMessageRepository)
        {
            _outboxMessageRepository = outboxMessageRepository;
        }

        public async Task Handle(PrioritySetted notification, CancellationToken cancellationToken)
        {
            EmployeeTaskPriorityUpdatedIntegrationEvent integrationEvent = new()
            {
                Id = notification.abstractScheduleItem.Id.Value.ToString(),
                Priority = notification.priorityEnum
            };
            await _outboxMessageRepository.AddOutboxMessageAsync(integrationEvent);
        }
    }
}