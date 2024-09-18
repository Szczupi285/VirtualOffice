using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.MeetingEvent
{
    public record MeetingStartDateUpdated(Meeting meeting, DateTime startDate) : IDomainEvent;
}