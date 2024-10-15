using AutoMapper;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Strategies.ScheduleItemDescriptionStrategies
{
    public class ScheduleItemDescriptionSettedStrategyFactory
    {
        private readonly IMapper _mapper;
        private readonly IOutboxMessageRepository _outboxMessagesRepository;

        private readonly Dictionary<Type, Func<IScheduleItemDescriptionSettedStrategy>> _strategies;

        public ScheduleItemDescriptionSettedStrategyFactory(IMapper mapper, IOutboxMessageRepository outboxMessageRepository)
        {
            _mapper = mapper;
            _outboxMessagesRepository = outboxMessageRepository;
            // Dict of all strategies
            _strategies = new Dictionary<Type, Func<IScheduleItemDescriptionSettedStrategy>>
            {
                {typeof(CalendarEvent), () => new CalendarEventDescriptionSettedStrategy(_mapper, _outboxMessagesRepository)},
                {typeof(EmployeeTask), () => new EmployeeTaskDescriptionSetterStrategy(_outboxMessagesRepository)},
            };
        }

        public IScheduleItemDescriptionSettedStrategy GetStrategy(Type type)
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