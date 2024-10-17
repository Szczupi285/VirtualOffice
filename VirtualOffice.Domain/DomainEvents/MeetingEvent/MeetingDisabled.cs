namespace VirtualOffice.Domain.DomainEvents.MeetingEvent
{
    public record MeetingDisabled(Guid Id) : IDomainEvent;
}