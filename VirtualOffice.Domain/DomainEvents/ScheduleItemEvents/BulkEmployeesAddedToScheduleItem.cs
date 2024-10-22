using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.ScheduleItemEvents
{
    public record BulkEmployeesAddedToScheduleItem(AbstractScheduleItem AbstractScheduleItem, HashSet<ApplicationUser> AddedEmployees, Type RaisingEntityType) : IDomainEvent;
}