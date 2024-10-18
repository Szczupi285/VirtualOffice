using AutoMapper;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Strategies.ScheduleItemRescheduledStrategies
{
    public class ScheduleItemRescheduledStrategyFactory
    {
        private readonly IMapper _mapper;
        private readonly IOutboxMessageRepository _outboxMessagesRepository;

        private readonly Dictionary<Type, Func<IScheduleItemRescheduledStrategy>> _strategies;

        public ScheduleItemRescheduledStrategyFactory(IMapper mapper, IOutboxMessageRepository outboxMessageRepository)
        {
            _mapper = mapper;
            _outboxMessagesRepository = outboxMessageRepository;
            // Dict of all strategies
            _strategies = new Dictionary<Type, Func<IScheduleItemRescheduledStrategy>>
            {
                {typeof(CalendarEvent), () => new CalendarEventRescheduledStrategy(_outboxMessagesRepository)},
                {typeof(EmployeeTask), () => new EmployeeTaskRescheduledStrategy(_outboxMessagesRepository)},
                {typeof(Meeting), () => new MeetingRescheduledStrategy(_outboxMessagesRepository)}
            };
        }

        public IScheduleItemRescheduledStrategy GetStrategy(Type type)
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