using AutoMapper;
using VirtualOffice.Application.IntegrationEvents.CalendarEventIntegrationEvents;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;
using VirtualOffice.Domain.DomainEvents.ScheduleItemEvents;

namespace VirtualOffice.Application.Strategies.ScheduleItemEmployeesStrategies.Add
{
    public class CalendarEventEmployeesAddedStrategy : IScheduleItemEmployeesAddedStrategy
    {
        private readonly IMapper _mapper;
        private readonly IOutboxMessageRepository _outboxMessageRepository;

        public CalendarEventEmployeesAddedStrategy(IMapper mapper, IOutboxMessageRepository outboxMessageRepository)
        {
            _mapper = mapper;
            _outboxMessageRepository = outboxMessageRepository;
        }

        public async Task Handle(BulkEmployeesAddedToScheduleItem notification, CancellationToken cancellationToken)
        {
            CalendarEventEmployeesAddedIntegrationEvent integrationEvent = new CalendarEventEmployeesAddedIntegrationEvent
            {
                Id = notification.AbstractScheduleItem.Id.Value.ToString(),
                AssignedEmployees = _mapper.Map<List<EmployeeReadModel>>(notification.AbstractScheduleItem._AssignedEmployees),
            };
            await _outboxMessageRepository.AddOutboxMessageAsync(integrationEvent);
        }
    }
}