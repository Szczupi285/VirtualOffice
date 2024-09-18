using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.ScheduleItemEvents
{
    public record EmployeeRemovedFromScheduleItem(AbstractScheduleItem abstractScheduleItem, ApplicationUser user) : IDomainEvent;
}