using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.MeetingEvent
{
    public record MeetingCreatedEvent(Meeting Meeting) : IDomainEvent;
}