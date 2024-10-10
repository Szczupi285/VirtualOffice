namespace VirtualOffice.Domain.DomainEvents.CalendarEventEvents
{
    public record CalendarEventDisabled(Guid Id) : IDomainEvent;
}