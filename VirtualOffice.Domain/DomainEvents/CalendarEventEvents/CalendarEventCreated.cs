using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Domain.DomainEvents.CalendarEventEvents
{
    public record CalendarEventCreated(ScheduleItemId Id, ScheduleItemTitle Title,
        ScheduleItemDescription Description, HashSet<ApplicationUser> AssignedEmployees,
        ScheduleItemStartDate StartDate, ScheduleItemEndDate EndDate) : IDomainEvent
    {
    }
}