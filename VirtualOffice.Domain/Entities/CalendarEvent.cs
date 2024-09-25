using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.DomainEvents.CalendarEventEvents;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;
using VIrtualOffice.Domain.Exceptions.ScheduleItem;

namespace VirtualOffice.Domain.Entities
{
    public class CalendarEvent : AbstractScheduleItem
    {
        public CalendarEvent(ScheduleItemId id, ScheduleItemTitle titile, ScheduleItemDescription eventDescription,
            HashSet<ApplicationUser> assignedEmployees, ScheduleItemStartDate startDate, ScheduleItemEndDate endDate)
            : base(id, titile, eventDescription, assignedEmployees, startDate, endDate)
        {
        }

        private CalendarEvent()
        { }

        public CalendarEvent Clone()
        {
            return new CalendarEvent(
                Id,
                _Title,
                _Description,
                new HashSet<ApplicationUser>(_AssignedEmployees), // Deep copy of assigned employees
                _StartDate,
                _EndDate
            )
            {
                Version = Version // Copy the version to maintain consistency for concurrency checks
            };
        }

        public void UpdateStartDate(DateTime startDate)
        {
            if (_EndDate <= startDate)
                throw new EndDateCannotBeBeforeStartDate(_EndDate, startDate);

            _StartDate = startDate;
            AddEvent(new CalendarEventStartDateUpdated(this, startDate));
        }
    }
}