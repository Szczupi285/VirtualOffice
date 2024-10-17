using AutoMapper;
using MediatR;
using VirtualOffice.Application.IntegrationEvents.MeetingIntegrationEvents;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;
using VirtualOffice.Domain.DomainEvents.MeetingEvent;

namespace VirtualOffice.Application.DomainEventHandlers.MeetingDomainEventHandlers
{
    internal class MeetingCreatedDomainEventHandler : INotificationHandler<MeetingCreatedEvent>
    {
        private readonly IOutboxMessageRepository _outboxMessageRepository;
        private readonly IMapper _mapper;

        public MeetingCreatedDomainEventHandler(IOutboxMessageRepository outboxMessageRepository, IMapper mapper)
        {
            _outboxMessageRepository = outboxMessageRepository;
            _mapper = mapper;
        }

        public async Task Handle(MeetingCreatedEvent notification, CancellationToken cancellationToken)
        {
            MeetingCreatedIntegrationEvent integrationEvent = new()
            {
                Id = notification.Meeting.Id.Value.ToString(),
                Title = notification.Meeting._Title,
                Description = notification.Meeting._Description,
                AssignedEmployees = _mapper.Map<List<EmployeeReadModel>>(notification.Meeting._AssignedEmployees),
                StartDate = notification.Meeting._StartDate,
                EndDate = notification.Meeting._EndDate,
            };
            await _outboxMessageRepository.AddOutboxMessageAsync(integrationEvent);
        }
    }
}