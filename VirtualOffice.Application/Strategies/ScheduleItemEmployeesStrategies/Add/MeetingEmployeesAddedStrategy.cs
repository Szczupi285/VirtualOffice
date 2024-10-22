using AutoMapper;
using VirtualOffice.Application.IntegrationEvents.MeetingIntegrationEvents;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;
using VirtualOffice.Domain.DomainEvents.ScheduleItemEvents;

namespace VirtualOffice.Application.Strategies.ScheduleItemEmployeesStrategies.Add
{
    internal class MeetingEmployeesAddedStrategy : IScheduleItemEmployeesAddedStrategy
    {
        private readonly IOutboxMessageRepository _outboxMessageRepository;
        private readonly IMapper _mapper;

        public MeetingEmployeesAddedStrategy(IMapper mapper, IOutboxMessageRepository outboxMessageRepository)
        {
            _mapper = mapper;
            _outboxMessageRepository = outboxMessageRepository;
        }

        public async Task Handle(BulkEmployeesAddedToScheduleItem notification, CancellationToken cancellationToken)
        {
            MeetingEmployeesAddedIntegrationEvent integrationEvent = new()
            {
                Id = notification.AbstractScheduleItem.Id.Value.ToString(),
                AssignedEmployees = _mapper.Map<List<EmployeeReadModel>>(notification.AddedEmployees),
            };
            await _outboxMessageRepository.AddOutboxMessageAsync(integrationEvent);
        }
    }
}