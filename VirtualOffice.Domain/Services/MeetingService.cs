using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Shared;

namespace VirtualOffice.Domain.Services
{
    public class MeetingService : AbstractScheduleItemService<Meeting>
    {
        public MeetingService(HashSet<Meeting> scheduleItems) : base(scheduleItems)
        {
        }

        public MeetingService(HashSet<Meeting> scheduleItems, IDateTimeProvider dateTimeProvider) : base(scheduleItems, dateTimeProvider)
        {
        }

        public ICollection<Meeting> GetAllEmployeeMeetings(ApplicationUser user)
           => _ScheduleItems.Where(meeting => meeting._AssignedEmployees.Contains(user)
           && meeting._EndDate >= _DateTimeProvider.UtcNow()).ToList();

        public ICollection<Meeting> GetMeetingsUntilDate(ApplicationUser user, DateTime endDate)
         => _ScheduleItems.Where(meeting => meeting._AssignedEmployees.Contains(user)
         && meeting._EndDate < endDate
         && meeting._EndDate >= _DateTimeProvider.UtcNow()).ToList();
    }
}