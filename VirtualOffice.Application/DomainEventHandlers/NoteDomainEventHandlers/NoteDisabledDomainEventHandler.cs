using MediatR;
using VirtualOffice.Application.IntegrationEvents.NoteIntegrationEvent;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Domain.DomainEvents.NoteEvents;

namespace VirtualOffice.Application.DomainEventHandlers.NoteDomainEventHandlers
{
    internal class NoteDisabledDomainEventHandler : INotificationHandler<NoteDisabled>
    {
        private readonly IOutboxMessageRepository _messageRepository;

        public NoteDisabledDomainEventHandler(IOutboxMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task Handle(NoteDisabled notification, CancellationToken cancellationToken)
        {
            NoteDisabledIntegrationEvent integrationEvent = new()
            {
                Id = notification.Id.ToString(),
            };

            await _messageRepository.AddOutboxMessageAsync(integrationEvent, cancellationToken);
        }
    }
}