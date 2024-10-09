using VirtualOffice.Domain.DomainEvents.ScheduleItemEvents;

namespace VirtualOffice.Application.Strategies.ScheduleItemDescriptionStrategies
{
    public interface IScheduleItemDescriptionSettedStrategy
    {
        Task Handle(ScheduleItemDescriptionSetted descriptionSetted, CancellationToken cancellationToken);
    }
}