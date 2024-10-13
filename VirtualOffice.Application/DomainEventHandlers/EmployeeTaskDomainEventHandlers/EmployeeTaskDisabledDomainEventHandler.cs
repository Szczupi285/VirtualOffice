using AutoMapper;
using MediatR;
using VirtualOffice.Application.IntegrationEvents.EmployeeTaskIntegrationEvents;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Domain.DomainEvents.EmployeeTask;

namespace VirtualOffice.Application.DomainEventHandlers.EmployeeTaskDomainEventHandlers
{
    public class EmployeeTaskDisabledDomainEventHandler : INotificationHandler<EmployeeTaskDisabled>
    {
        private readonly IOutboxMessageRepository _outboxMessageRepository;

        public EmployeeTaskDisabledDomainEventHandler(IMapper mapper, IOutboxMessageRepository outboxMessageRepository)
        {
            _outboxMessageRepository = outboxMessageRepository;
        }

        public async Task Handle(EmployeeTaskDisabled notification, CancellationToken cancellationToken)
        {
            EmployeeTaskDeletedIntegrationEvent integrationEvent = new EmployeeTaskDeletedIntegrationEvent
            {
                Id = notification.Id,
            };
            await _outboxMessageRepository.AddOutboxMessageAsync(integrationEvent);
        }
    }
}