﻿using AutoMapper;
using MediatR;
using VirtualOffice.Application.IntegrationEvents.CalendarEventIntegrationEvents;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;
using VirtualOffice.Domain.DomainEvents.CalendarEventEvents;

namespace VirtualOffice.Application.DomainEventHandlers.CalendarEventDomainEventHandlers
{
    public class RescheduleCalendarEventDomainEventHandler : INotificationHandler<CalendarEventRescheduled>
    {
        private readonly IEventBus _eventBus;
        private readonly IMapper _mapper;
        private readonly IOutboxMessageRepository _outboxMessageRepository;

        public RescheduleCalendarEventDomainEventHandler(IEventBus eventBus, IMapper mapper, IOutboxMessageRepository outboxMessageRepository)
        {
            _eventBus = eventBus;
            _mapper = mapper;
            _outboxMessageRepository = outboxMessageRepository;
        }

        public async Task Handle(CalendarEventRescheduled notification, CancellationToken cancellationToken)
        {
            CalendarEventRescheduledIntegrationEvent integrationEvent = new CalendarEventRescheduledIntegrationEvent
            {
                Id = notification.CalendarEvent.Id.Value.ToString(),
                Title = notification.CalendarEvent._Title,
                Description = notification.CalendarEvent._Description,
                AssignedEmployees = _mapper.Map<List<EmployeeReadModel>>(notification.CalendarEvent._AssignedEmployees),
                StartDate = notification.CalendarEvent._StartDate,
                EndDate = notification.CalendarEvent._EndDate,
            };

            await _outboxMessageRepository.AddOutboxMessageAsync(integrationEvent);
        }
    }
}