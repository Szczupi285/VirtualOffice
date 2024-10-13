using AutoMapper;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Strategies.ScheduleItemEmployeesStrategies.Add
{
    public class ScheduleItemEmployeesAddedStrategyFactory
    {
        private readonly IMapper _mapper;
        private readonly IOutboxMessageRepository _outboxMessagesRepository;

        private readonly Dictionary<Type, Func<IScheduleItemEmployeesAddedStrategy>> _strategies;

        public ScheduleItemEmployeesAddedStrategyFactory(IMapper mapper, IOutboxMessageRepository outboxMessageRepository)
        {
            _mapper = mapper;
            _outboxMessagesRepository = outboxMessageRepository;
            // Dict of all strategies
            _strategies = new Dictionary<Type, Func<IScheduleItemEmployeesAddedStrategy>>
            {
                {typeof(CalendarEvent), () => new CalendarEventEmployeesAddedStrategy(_mapper, _outboxMessagesRepository)},
                {typeof(EmployeeTask), () => new EmployeeTaskEmployeesAddedStrategy(_mapper, _outboxMessagesRepository)}
            };
        }

        public IScheduleItemEmployeesAddedStrategy GetStrategy(Type type)
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