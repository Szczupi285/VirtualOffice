using VirtualOffice.Domain.DomainEvents.ScheduleItemEvents;

namespace VirtualOffice.Application.Strategies.ScheduleItemRescheduledStrategies
{
    public interface IScheduleItemRescheduledStrategy
    {
        Task Handle(ScheduleItemRescheduled notification, CancellationToken cancellationToken);
    }
}