using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.ScheduleItemEvents
{
    public record BulkEmployeesRemovedFromScheduleItem(AbstractScheduleItem AbstractScheduleItem, HashSet<ApplicationUser> RemovedEmployees, Type RaisingEntityType) : IDomainEvent;
}