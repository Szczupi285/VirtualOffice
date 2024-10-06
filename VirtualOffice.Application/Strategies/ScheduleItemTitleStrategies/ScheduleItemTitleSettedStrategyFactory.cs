using AutoMapper;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Strategies.ScheduleItemTitleStrategies
{
    public class ScheduleItemTitleSettedStrategyFactory
    {
        private readonly IMapper _mapper;
        private readonly IOutboxMessageRepository _outboxMessagesRepository;

        private readonly Dictionary<Type, Func<IScheduleItemTitleSettedStrategy>> _strategies;

        public ScheduleItemTitleSettedStrategyFactory(IMapper mapper, IOutboxMessageRepository outboxMessageRepository)
        {
            _mapper = mapper;
            _outboxMessagesRepository = outboxMessageRepository;
            // Dict of all strategies
            _strategies = new Dictionary<Type, Func<IScheduleItemTitleSettedStrategy>>
            {
                {typeof(CalendarEvent), () => new CalendarEventTitleSettedStrategy(_mapper, _outboxMessagesRepository)},
            };
        }

        public IScheduleItemTitleSettedStrategy GetStrategy(Type type)
        {
            // try to acces the strategy associated with the given type
            if (_strategies.TryGetValue(type, out var strategy))
            {
                return strategy();
            }
            // return default strategy or handle exception
            throw new NotImplementedException();
        }
    }
}