using AutoMapper;
using MediatR;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;
using VirtualOffice.Domain.DomainEvents.CalendarEventEvents;

namespace VirtualOffice.Application.Events
{
    internal sealed class CalendarEventCreatedDomainEventHandler
        : INotificationHandler<CalendarEventCreated>
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
            await _eventBus.PublishAsync(new CalendarEventCreatedIntegrationEvent
            {
                Id = notification.Id.Value.ToString(),
                Title = notification.Title,
                Description = notification.Description,
                AssignedEmployees = _mapper.Map<List<EmployeeReadModel>>(notification.AssignedEmployees),
                StartDate = notification.StartDate,
                EndDate = notification.EndDate,
            }
            , cancellationToken);
        }
    }
}