using VirtualOffice.Domain.DomainEvents.ScheduleItemEvents;

namespace VirtualOffice.Application.Strategies.ScheduleItemTitleStrategies
{
    public interface IScheduleItemTitleSettedStrategy
    {
        Task Handle(ScheduleItemTitleSetted titleSetted, CancellationToken cancellationToken);
    }
}