﻿using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Domain.Repositories
{
    public interface ICalendarEventRepository
    {
        Task<CalendarEvent> GetByIdAsync(ScheduleItemId guid, CancellationToken cancellationToken);

        Task AddAsync(CalendarEvent calendarEvent, CancellationToken cancellationToken);

        Task DeleteAsync(CalendarEvent calendarEvent, CancellationToken cancellationToken);

        Task UpdateAsync(CalendarEvent calendarEvent, CancellationToken cancellationToken);
    }
}