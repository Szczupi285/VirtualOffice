﻿using AutoMapper;
using MediatR;
using VirtualOffice.Application.Events;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;
using VirtualOffice.Domain.DomainEvents.CalendarEventEvents;

namespace VirtualOffice.Application.DomainEventHandlers
{
    public class CalendarEventCreatedDomainEventHandler : INotificationHandler<CalendarEventCreated>
    {
        private readonly IEventBus _eventBus;
        private readonly IMapper _mapper;

        public CalendarEventCreatedDomainEventHandler(IEventBus eventBus, IMapper mapper)
        {
            _eventBus = eventBus;
            _mapper = mapper;
        }

        public async Task Handle(CalendarEventCreated notification, CancellationToken cancellationToken)
        {
            await _eventBus.PublishAsync(new CalendarEventCreatedEvent
            {
                Id = notification.CalendarEvent.Id.Value.ToString(),
                Title = notification.CalendarEvent._Title.Value,
                Description = notification.CalendarEvent._Description.Value,
                AssignedEmployees = _mapper.Map<List<EmployeeReadModel>>(notification.CalendarEvent._AssignedEmployees),
                StartDate = notification.CalendarEvent._StartDate.Value,
                EndDate = notification.CalendarEvent._EndDate.Value,
            }
           , cancellationToken);
        }
    }
}