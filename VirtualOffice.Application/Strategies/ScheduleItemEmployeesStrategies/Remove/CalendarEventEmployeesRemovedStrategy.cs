using AutoMapper;
using VirtualOffice.Application.IntegrationEvents.CalendarEventIntegrationEvents;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;
using VirtualOffice.Domain.DomainEvents.ScheduleItemEvents;

namespace VirtualOffice.Application.Strategies.ScheduleItemEmployeesStrategies.Remove
{
    public class CalendarEventEmployeesRemovedStrategy : IScheduleItemEmployeesRemovedStrategy
    {
        private readonly IMapper _mapper;
        private readonly IOutboxMessageRepository _outboxMessageRepository;

        public CalendarEventEmployeesRemovedStrategy(IMapper mapper, IOutboxMessageRepository outboxMessageRepository)
        {
            _mapper = mapper;
            _outboxMessageRepository = outboxMessageRepository;
        }

        public async Task Handle(BulkEmployeesRemovedFromScheduleItem notification, CancellationToken cancellationToken)
        {
            CalendarEventEmployeesRemovedIntegrationEvent integrationEvent = new CalendarEventEmployeesRemovedIntegrationEvent
            {
                Id = notification.AbstractScheduleItem.Id.Value.ToString(),
                AssignedEmployees = _mapper.Map<List<EmployeeReadModel>>(notification.AbstractScheduleItem._AssignedEmployees),
            };
            await _outboxMessageRepository.AddOutboxMessageAsync(integrationEvent);
        }
    }
}