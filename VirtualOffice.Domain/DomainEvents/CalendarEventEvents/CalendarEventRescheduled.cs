using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.CalendarEventEvents
{
    public record CalendarEventRescheduled(CalendarEvent CalendarEvent) : IDomainEvent;
}