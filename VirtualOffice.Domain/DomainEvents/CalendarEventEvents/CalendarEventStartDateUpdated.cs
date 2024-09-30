using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.CalendarEventEvents
{
    public record CalendarEventStartDateUpdated(CalendarEvent calendarEvent) : IDomainEvent;
}