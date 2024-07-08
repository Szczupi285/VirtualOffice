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
        Task<CalendarEvent> GetById(ScheduleItemId guid);
        Task Add(CalendarEvent calendarEvent);
        Task Update(CalendarEvent calendarEvent);
        Task Delete(ScheduleItemId guid);
        Task<IEnumerable<CalendarEvent>> GetAllForUser(ApplicationUserId userId);
        Task<IEnumerable<CalendarEvent>> GetAllForUserFutureEvents(ApplicationUserId userId);
        Task<IEnumerable<CalendarEvent>> GetAllForUserByDate(ApplicationUserId userId, ScheduleItemStartDate startDate, ScheduleItemEndDate endDate);
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
