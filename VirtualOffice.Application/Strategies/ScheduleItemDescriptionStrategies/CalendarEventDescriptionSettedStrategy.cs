using AutoMapper;
using VirtualOffice.Application.IntegrationEvents;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;
using VirtualOffice.Domain.DomainEvents.ScheduleItemEvents;

namespace VirtualOffice.Application.Strategies.ScheduleItemDescriptionStrategies
{
    public class CalendarEventDescriptionSettedStrategy : IScheduleItemDescriptionSettedStrategy
    {
        private readonly IMapper _mapper;
        private readonly IOutboxMessageRepository _outboxMessageRepository;

        public CalendarEventDescriptionSettedStrategy(IMapper mapper, IOutboxMessageRepository outboxMessageRepository)
        {
            _mapper = mapper;
            _outboxMessageRepository = outboxMessageRepository;
        }

        public async Task Handle(ScheduleItemDescriptionSetted notification, CancellationToken cancellationToken)
        {
            CalendarEventDescriptionUpdatedIntegrationEvent integrationEvent = new CalendarEventDescriptionUpdatedIntegrationEvent
            {
                Id = notification.abstractScheduleItem.Id.Value.ToString(),
                Title = notification.abstractScheduleItem._Title,
                Description = notification.abstractScheduleItem._Description,
                AssignedEmployees = _mapper.Map<List<EmployeeReadModel>>(notification.abstractScheduleItem._AssignedEmployees),
                StartDate = notification.abstractScheduleItem._StartDate,
                EndDate = notification.abstractScheduleItem._EndDate,
            };

            await _outboxMessageRepository.AddOutboxMessageAsync(integrationEvent);
        }
    }
}