using AutoMapper;
using VirtualOffice.Application.IntegrationEvents.MeetingIntegrationEvents;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;
using VirtualOffice.Domain.DomainEvents.ScheduleItemEvents;

namespace VirtualOffice.Application.Strategies.ScheduleItemEmployeesStrategies.Remove
{
    internal class MeetingEmployeesRemovedStrategy : IScheduleItemEmployeesRemovedStrategy
    {
        private readonly IMapper _mapper;
        private readonly IOutboxMessageRepository _outboxMessageRepository;

        public MeetingEmployeesRemovedStrategy(IMapper mapper, IOutboxMessageRepository outboxMessageRepository)
        {
            _mapper = mapper;
            _outboxMessageRepository = outboxMessageRepository;
        }

        public async Task Handle(BulkEmployeesRemovedFromScheduleItem notification, CancellationToken cancellationToken)
        {
            MeetingEmployeesRemovedIntegrationEvent integrationEvent = new()
            {
                Id = notification.AbstractScheduleItem.Id.Value.ToString(),
                AssignedEmployees = _mapper.Map<List<EmployeeReadModel>>(notification.RemovedEmployees),
            };
            await _outboxMessageRepository.AddOutboxMessageAsync(integrationEvent);
        }
    }
}