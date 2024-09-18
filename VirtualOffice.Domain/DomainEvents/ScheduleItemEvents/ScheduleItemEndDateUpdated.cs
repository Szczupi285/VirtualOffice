using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Domain.DomainEvents.ScheduleItemEvents
{
    public record ScheduleItemEndDateUpdated(AbstractScheduleItem abstractScheduleItem, ScheduleItemEndDate EndDate) : IDomainEvent;
}