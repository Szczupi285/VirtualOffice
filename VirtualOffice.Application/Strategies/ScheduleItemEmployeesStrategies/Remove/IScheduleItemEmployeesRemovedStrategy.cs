using VirtualOffice.Domain.DomainEvents.ScheduleItemEvents;

namespace VirtualOffice.Application.Strategies.ScheduleItemEmployeesStrategies.Remove
{
    public interface IScheduleItemEmployeesRemovedStrategy
    {
        Task Handle(BulkEmployeesRemovedFromScheduleItem employeesRemoved, CancellationToken cancellationToken);
    }
}