using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Domain.Repositories
{
    public interface ICalendarEventRepository
    {
        CalendarEvent GetById(ScheduleItemId guid);
        void Add(CalendarEvent calendarEvent);
        void Update(CalendarEvent calendarEvent);
        void Delete(ScheduleItemId guid);
        IEnumerable<CalendarEvent> GetAllForUser(ApplicationUserId userId);
        IEnumerable<CalendarEvent> GetAllForUserFutureEvents(ApplicationUserId userId);
        IEnumerable<CalendarEvent> GetAllForUserByDate(ApplicationUserId userId, ScheduleItemStartDate startDate, ScheduleItemEndDate endDate);
    }
}
