﻿using AutoMapper;
using MediatR;
using VirtualOffice.Application.Commands.CalendarEventCommands;
using VirtualOffice.Application.Events;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.CalendarEventHandlers
{
    public class CreateCalendarEventHandler : IRequestHandler<CreateCalendarEvent>
    {
        private readonly ICalendarEventRepository _repository;
        private readonly IEventBus _eventBus;
        private readonly IMapper _mapper;

        public CreateCalendarEventHandler(ICalendarEventRepository repository, IEventBus eventBus, IMapper mapper)
        {
            _repository = repository;
            _eventBus = eventBus;
            _mapper = mapper;
        }

        public async Task Handle(CreateCalendarEvent request, CancellationToken cancellationToken)
        {
            var (Title, EventDescription, AssignedEmployees, StartDate, EndDate) = request;
            Guid guid = Guid.NewGuid();

            CalendarEvent calEv = new CalendarEvent(guid, Title, EventDescription, AssignedEmployees, StartDate, EndDate);

            await _repository.AddAsync(calEv);

            await _eventBus.PublishAsync(new CalendarEventCreatedEvent
            {
                Id = guid.ToString(),
                Title = request.Title,
                Description = request.Description,
                AssignedEmployees = _mapper.Map<List<EmployeeReadModel>>(request.AssignedEmployees),
                StartDate = request.StartDate,
                EndDate = request.EndDate,
            }
            , cancellationToken);
        }
    }
}