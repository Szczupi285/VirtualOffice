using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Domain.DomainEvents.CalendarEventEvents
{
    public record CalendarEventStartDateUpdated(CalendarEvent calendarEvent, ScheduleItemStartDate StartDate) : IDomainEvent;
}