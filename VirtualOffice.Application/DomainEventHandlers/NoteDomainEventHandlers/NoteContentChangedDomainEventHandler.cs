using MediatR;
using VirtualOffice.Application.IntegrationEvents.NoteIntegrationEvent;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Domain.DomainEvents.NoteEvent;

namespace VirtualOffice.Application.DomainEventHandlers.NoteDomainEventHandlers
{
    internal class NoteContentChangedDomainEventHandler : INotificationHandler<NoteContentChanged>
    {
        private readonly IOutboxMessageRepository _messageRepository;

        public NoteContentChangedDomainEventHandler(IOutboxMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task Handle(NoteContentChanged notification, CancellationToken cancellationToken)
        {
            NoteContentChangedIntegrationEvent integrationEvent = new()
            {
                Id = notification.note.Id.Value.ToString(),
                Content = notification.note._content
            };

            await _messageRepository.AddOutboxMessageAsync(integrationEvent, cancellationToken);

        }
    }
}
