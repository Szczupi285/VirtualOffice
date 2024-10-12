using VirtualOffice.Domain.DomainEvents.ScheduleItemEvents;

namespace VirtualOffice.Application.Strategies.ScheduleItemEmployeesStrategies.Add
{
    public interface IScheduleItemEmployeesAddedStrategy
    {
        Task Handle(BulkEmployeesAddedToScheduleItem employeesAdded, CancellationToken cancellationToken);
    }
}