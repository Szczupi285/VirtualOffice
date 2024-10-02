using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.DomainEvents.CalendarEventEvents;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;
using VIrtualOffice.Domain.Exceptions.ScheduleItem;

namespace VirtualOffice.Domain.Entities
{
    public class CalendarEvent : AbstractScheduleItem
    {
        public CalendarEvent(ScheduleItemId id, ScheduleItemTitle title, ScheduleItemDescription eventDescription,
            HashSet<ApplicationUser> assignedEmployees, ScheduleItemStartDate startDate, ScheduleItemEndDate endDate)
            : base(id, title, eventDescription, assignedEmployees, startDate, endDate)
        {
            AddEvent(new CalendarEventCreated(id, title, eventDescription, assignedEmployees.ToList(), startDate, endDate));
        }

        private CalendarEvent()
        { }

        public void UpdateStartDate(DateTime startDate)
        {
            if (_EndDate <= startDate)
                throw new EndDateCannotBeBeforeStartDate(_EndDate, startDate);

            _StartDate = startDate;
            AddEvent(new CalendarEventStartDateUpdated(this, startDate));
        }
    }
}