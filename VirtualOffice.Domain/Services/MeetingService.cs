using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;
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
