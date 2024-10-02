using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.CalendarEventEvents
{
    public record CalendarEventCreated(CalendarEvent CalendarEvent) : IDomainEvent;
}