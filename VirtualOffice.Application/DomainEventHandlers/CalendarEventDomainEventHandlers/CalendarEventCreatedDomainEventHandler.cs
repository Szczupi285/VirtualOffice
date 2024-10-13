using AutoMapper;
using MediatR;
using VirtualOffice.Application.IntegrationEvents.CalendarEventIntegrationEvents;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;
using VirtualOffice.Domain.DomainEvents.CalendarEventEvents;

namespace VirtualOffice.Application.DomainEventHandlers.CalendarEventDomainEventHandlers
{
    public class CalendarEventCreatedDomainEventHandler : INotificationHandler<CalendarEventCreated>
    {
        private readonly IEventBus _eventBus;
        private readonly IMapper _mapper;
        private readonly IOutboxMessageRepository _outboxMessageRepository;

        public CalendarEventCreatedDomainEventHandler(IEventBus eventBus, IMapper mapper, IOutboxMessageRepository outboxMessageRepository)
        {
            _eventBus = eventBus;
            _mapper = mapper;
            _outboxMessageRepository = outboxMessageRepository;
        }

        public async Task Handle(CalendarEventCreated notification, CancellationToken cancellationToken)
        {
            CalendarEventCreatedIntegrationEvent integrationEvent = new CalendarEventCreatedIntegrationEvent
            {
                Id = notification.Id.ToString(),
                Title = notification.Title,
                Description = notification.Description,
                AssignedEmployees = _mapper.Map<List<EmployeeReadModel>>(notification.AssignedEmployees),
                StartDate = notification.StartDate,
                EndDate = notification.EndDate,
            };

            await _outboxMessageRepository.AddOutboxMessageAsync(integrationEvent);
        }
    }
}