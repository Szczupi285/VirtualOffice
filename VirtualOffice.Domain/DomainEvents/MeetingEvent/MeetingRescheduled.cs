using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.MeetingEvent
{
    public record MeetingRescheduled(Meeting Meeting, DateTime startDate, DateTime endDate) : IDomainEvent;
}