using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.ScheduleItem
{
    public record EmployeeAddedToScheduleItem(AbstractScheduleItem abstractScheduleItem, ApplicationUser user) : IDomainEvent;
}