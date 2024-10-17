using MediatR;
using VirtualOffice.Application.IntegrationEvents.EmployeeTaskIntegrationEvents;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Domain.DomainEvents.EmployeeTask;

namespace VirtualOffice.Application.DomainEventHandlers.EmployeeTaskDomainEventHandlers
{
    internal sealed class StatusUpdatedDomainEventHandler : INotificationHandler<StatusUpdated>
    {
        private readonly IOutboxMessageRepository _outboxMessageRepository;

        public StatusUpdatedDomainEventHandler(IOutboxMessageRepository outboxMessageRepository)
        {
            _outboxMessageRepository = outboxMessageRepository;
        }

        public async Task Handle(StatusUpdated notification, CancellationToken cancellationToken)
        {
            EmployeeTaskStatusUpdatedIntegrationEvent integrationEvent = new()
            {
                Id = notification.abstractScheduleItem.Id.Value.ToString(),
                Status = notification.statusEnum
            };
            await _outboxMessageRepository.AddOutboxMessageAsync(integrationEvent);
        }
    }
}