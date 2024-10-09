using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.CalendarEventEvents
{
    public class CalendarEventRescheduled(CalendarEvent calendarEvent) : IDomainEvent;
}