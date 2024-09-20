﻿using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Domain.Repositories
{
    public interface ICalendarEventRepository
    {
        Task<CalendarEvent> GetById(ScheduleItemId guid);

        Task<CalendarEvent> GetById(ScheduleItemId guid, CancellationToken cancellationToken);

        Task AddAsync(CalendarEvent calendarEvent);

        Task AddAsync(CalendarEvent calendarEvent, CancellationToken cancellationToken);

        Task DeleteAsync(CalendarEvent calendarEvent);

        Task DeleteAsync(CalendarEvent calendarEvent, CancellationToken cancellationToken);

        Task UpdateAsync(CalendarEvent calendarEvent);

        Task UpdateAsync(CalendarEvent calendarEvent, CancellationToken cancellationToken);
    }
}