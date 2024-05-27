using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.Repositories
{
    public interface ICalendarEventRepository
    {
        CalendarEvent GetById(Guid guid);
        void Add(CalendarEvent user);
        void Update(CalendarEvent user);
        void Delete(Guid id);
        IEnumerable<CalendarEvent> GetAllForUser(Guid userId);
        IEnumerable<CalendarEvent> GetAllForUserFutureEvents(Guid userId);
        IEnumerable<CalendarEvent> GetAllForUserInCertainPeriod(Guid userId, DateTime startDate, DateTime endDate);
    }
}
