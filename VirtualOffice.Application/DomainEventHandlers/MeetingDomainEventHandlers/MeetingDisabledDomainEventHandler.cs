using MediatR;
using VirtualOffice.Application.IntegrationEvents.MeetingIntegrationEvents;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Domain.DomainEvents.MeetingEvent;

namespace VirtualOffice.Application.DomainEventHandlers.MeetingDomainEventHandlers
{
    internal sealed class MeetingDisabledDomainEventHandler : INotificationHandler<MeetingDisabled>
    {
        private readonly IOutboxMessageRepository _outboxMessageRepository;

        public MeetingDisabledDomainEventHandler(IOutboxMessageRepository outboxMessageRepository)
        {
            _outboxMessageRepository = outboxMessageRepository;
        }

        public async Task Handle(MeetingDisabled notification, CancellationToken cancellationToken)
        {
            MeetingDeletedIntegrationEvent integrationEvent = new()
            {
                Id = notification.Id.ToString(),
            };
            await _outboxMessageRepository.AddOutboxMessageAsync(integrationEvent);
        }
    }
}