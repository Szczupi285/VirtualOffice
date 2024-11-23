using MediatR;
using VirtualOffice.Application.IntegrationEvents.NoteIntegrationEvent;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Domain.DomainEvents.NoteEvent;

namespace VirtualOffice.Application.DomainEventHandlers.NoteDomainEventHandlers
{
    internal class NoteTitleChangedDomainEventHandler : INotificationHandler<NoteTitleChanged>
    {
        private readonly IOutboxMessageRepository _messageRepository;

        public NoteTitleChangedDomainEventHandler(IOutboxMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task Handle(NoteTitleChanged notification, CancellationToken cancellationToken)
        {
            NoteTitleChangedIntegrationEvent integrationEvent = new()
            {
                Id = notification.note.Id.Value.ToString(),
                Title = notification.note._title
            };

            await _messageRepository.AddOutboxMessageAsync(integrationEvent, cancellationToken);
        }
    }
}