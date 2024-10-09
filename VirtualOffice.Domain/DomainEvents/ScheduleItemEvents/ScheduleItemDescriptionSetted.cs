using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Domain.DomainEvents.ScheduleItemEvents
{
    public record ScheduleItemDescriptionSetted(AbstractScheduleItem abstractScheduleItem, ScheduleItemDescription description, Type RaisingEntityType) : IDomainEvent;
}