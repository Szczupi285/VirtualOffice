namespace VirtualOffice.Domain.DomainEvents.CalendarEventEvents
{
    public record CalendarEventTitleUpdated(Guid Id, string Title) : IDomainEvent;
}