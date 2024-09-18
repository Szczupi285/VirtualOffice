using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Domain.DomainEvents.ScheduleItemEvents
{
    public record ScheduleItemTitleSetted(AbstractScheduleItem abstractScheduleItem, ScheduleItemTitle title) : IDomainEvent;
}