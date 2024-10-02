using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.CalendarEventEvents
{
    public record CalendarEventCreated(Guid Id, string Title, string Description, List<ApplicationUser> AssignedEmployees, DateTime StartDate, DateTime EndDate) : IDomainEvent;
}