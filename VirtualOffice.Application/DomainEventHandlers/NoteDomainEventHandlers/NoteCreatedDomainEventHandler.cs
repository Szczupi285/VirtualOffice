using AutoMapper;
using MediatR;
using VirtualOffice.Application.IntegrationEvents.NoteCreatedIntegrationEvent;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;
using VirtualOffice.Domain.DomainEvents.NoteEvents;

namespace VirtualOffice.Application.DomainEventHandlers.NoteDomainEventHandlers
{
    internal class NoteCreatedDomainEventHandler : INotificationHandler<NoteCreated>
    {
        private readonly IOutboxMessageRepository _outboxMessageRepository;
        private readonly IMapper _mapper;

        public NoteCreatedDomainEventHandler(IOutboxMessageRepository outboxMessageRepository, IMapper mapper)
        {
            _outboxMessageRepository = outboxMessageRepository;
            _mapper = mapper;
        }

        public async Task Handle(NoteCreated notification, CancellationToken cancellationToken)
        {
            NoteCreatedIntegrationEvent integrationEvent = new()
            {
                Id = notification.Id.ToString(),
                Title = notification.Title,
                Content = notification.Content,
                CreatedBy = _mapper.Map<EmployeeReadModel>(notification.User),
            };
            await _outboxMessageRepository.AddOutboxMessageAsync(integrationEvent, cancellationToken);
        }
    }
}