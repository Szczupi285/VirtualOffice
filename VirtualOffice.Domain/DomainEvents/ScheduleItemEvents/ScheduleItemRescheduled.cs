using VirtualOffice.Domain.Abstractions;

namespace VirtualOffice.Domain.DomainEvents.ScheduleItemEvents
{
    public record ScheduleItemRescheduled(AbstractScheduleItem AbstractScheduleItem, DateTime StartDate, DateTime EndDate, Type RaisingEntityType) : IDomainEvent;
}
