using AutoMapper;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Strategies.ScheduleItemEmployeesStrategies.Remove
{
    public class ScheduleItemEmployeesRemovedStrategyFactory
    {
        private readonly IOutboxMessageRepository _outboxMessagesRepository;
        private readonly IMapper _mapper;

        private readonly Dictionary<Type, Func<IScheduleItemEmployeesRemovedStrategy>> _strategies;

        public ScheduleItemEmployeesRemovedStrategyFactory(IOutboxMessageRepository outboxMessagesRepository, IMapper mapper)
        {
            _outboxMessagesRepository = outboxMessagesRepository;
            _mapper = mapper;
            _strategies = new Dictionary<Type, Func<IScheduleItemEmployeesRemovedStrategy>>
            {
                { typeof(CalendarEvent), () => new CalendarEventEmployeesRemovedStrategy(_mapper, _outboxMessagesRepository) },
            };
        }

        public IScheduleItemEmployeesRemovedStrategy GetStrategy(Type type)
        {
            if (_strategies.TryGetValue(type, out var strategy))
            {
                return strategy();
            }
            // return default strategy or handle exception
            throw new NotImplementedException();
        }
    }
}