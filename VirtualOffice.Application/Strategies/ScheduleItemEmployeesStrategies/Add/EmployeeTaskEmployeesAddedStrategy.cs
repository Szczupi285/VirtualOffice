using AutoMapper;
using VirtualOffice.Application.IntegrationEvents.EmployeeTaskIntegrationEvents;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;
using VirtualOffice.Domain.DomainEvents.ScheduleItemEvents;

namespace VirtualOffice.Application.Strategies.ScheduleItemEmployeesStrategies.Add
{
    public class EmployeeTaskEmployeesAddedStrategy : IScheduleItemEmployeesAddedStrategy
    {
        private readonly IMapper _mapper;
        private readonly IOutboxMessageRepository _outboxMessageRepository;

        public EmployeeTaskEmployeesAddedStrategy(IMapper mapper, IOutboxMessageRepository outboxMessageRepository)
        {
            _mapper = mapper;
            _outboxMessageRepository = outboxMessageRepository;
        }

        public async Task Handle(BulkEmployeesAddedToScheduleItem notification, CancellationToken cancellationToken)
        {
            EmployeeTaskEmployeesAddedIntegrationEvent integrationEvent = new EmployeeTaskEmployeesAddedIntegrationEvent
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