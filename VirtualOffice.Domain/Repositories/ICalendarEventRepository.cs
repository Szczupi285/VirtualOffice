using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Domain.Repositories
{
    public interface ICalendarEventRepository
    {
        Task<CalendarEvent> GetByIdAsync(ScheduleItemId guid, CancellationToken cancellationToken = default);

        Task AddAsync(CalendarEvent calendarEvent, CancellationToken cancellationToken = default);

        Task DeleteAsync(CalendarEvent calendarEvent, CancellationToken cancellationToken = default);

        Task UpdateAsync(CalendarEvent calendarEvent, CancellationToken cancellationToken = default);
    }
}