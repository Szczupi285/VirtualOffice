using AutoMapper;
using VirtualOffice.Application.Events;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;
using VirtualOffice.Domain.DomainEvents.ScheduleItemEvents;

namespace VirtualOffice.Application.Strategies.ScheduleItemEmployeesStrategies
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
                Title = notification.AbstractScheduleItem._Title,
                Description = notification.AbstractScheduleItem._Description,
                AssignedEmployees = _mapper.Map<List<EmployeeReadModel>>(notification.AbstractScheduleItem._AssignedEmployees),
                StartDate = notification.AbstractScheduleItem._StartDate,
                EndDate = notification.AbstractScheduleItem._EndDate,
            };
            await _outboxMessageRepository.AddOutboxMessageAsync(integrationEvent);
        }
    }
}