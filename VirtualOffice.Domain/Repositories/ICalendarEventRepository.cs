using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Interfaces;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Domain.Repositories
{
    public interface ICalendarEventRepository
    {
        Task<ICalendarEvent> GetById(ScheduleItemId guid);
        Task Add(ICalendarEvent calendarEvent);
        Task Update(ICalendarEvent calendarEvent);
        Task Delete(ScheduleItemId guid);
        Task<IEnumerable<ICalendarEvent>> GetAllForUser(ApplicationUserId userId);
        Task<IEnumerable<ICalendarEvent>> GetAllForUserFutureEvents(ApplicationUserId userId);
        Task<IEnumerable<ICalendarEvent>> GetAllForUserByDate(ApplicationUserId userId, ScheduleItemStartDate startDate, ScheduleItemEndDate endDate);
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
