using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.DomainEvents.MeetingEvent;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;
using VIrtualOffice.Domain.Exceptions.ScheduleItem;

namespace VirtualOffice.Domain.Entities
{
    public class Meeting : AbstractScheduleItem
    {
        public Meeting(ScheduleItemId id, ScheduleItemTitle title, ScheduleItemDescription description,
            HashSet<ApplicationUser> assignedEmployees, ScheduleItemStartDate startDate, ScheduleItemEndDate endDate)
            : base(id, title, description, assignedEmployees, startDate, endDate)
        {
        }

        private Meeting()
        { }

        public void UpdateStartDate(DateTime newStartDate)
        {
            if (newStartDate > _EndDate)
                throw new EndDateCannotBeBeforeStartDate(newStartDate, _EndDate);
            _StartDate = newStartDate;
            AddEvent(new MeetingStartDateUpdated(this, newStartDate));
        }

        public void RescheduleMeeting(DateTime newStartDate, DateTime newEndDate)
        {
            if (newStartDate > newEndDate)
                throw new EndDateCannotBeBeforeStartDate(newStartDate, newEndDate);
            _EndDate = newEndDate;
            _StartDate = newStartDate;
            AddEvent(new MeetingRescheduled(this, newStartDate, newEndDate));
        }
    }
}