using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.Repositories;
using VirtualOffice.Domain.Repositories;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Infrastructure.EF.Repositories
{
    public class CalendarEventRepository : ICalendarEventRepository
    {
        private readonly WriteDbContext _dbContext;

        public CalendarEventRepository(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CalendarEvent> GetById(ScheduleItemId guid)
           => await _dbContext.CalendarEvents.
           Include(c => c._AssignedEmployees)
           .FirstOrDefaultAsync(c => c.Id == guid) ?? throw new CalendarEventNotFoundException(guid);

        public async Task<CalendarEvent> GetById(ScheduleItemId guid, CancellationToken cancellationToken)
            => await _dbContext.CalendarEvents.
            Include(c => c._AssignedEmployees)
            .FirstOrDefaultAsync(c => c.Id == guid, cancellationToken) ?? throw new CalendarEventNotFoundException(guid);

        public async Task AddAsync(CalendarEvent calendarEvent)
        {
            await _dbContext.CalendarEvents.AddAsync(calendarEvent);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAsync(CalendarEvent calendarEvent, CancellationToken cancellationToken)
        {
            await _dbContext.CalendarEvents.AddAsync(calendarEvent, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(CalendarEvent calendarEvent)
        {
            _dbContext.Remove(calendarEvent);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(CalendarEvent calendarEvent, CancellationToken cancellationToken)
        {
            _dbContext.Remove(calendarEvent);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(CalendarEvent calendarEvent)
        {
            _dbContext.CalendarEvents.Update(calendarEvent);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(CalendarEvent calendarEvent, CancellationToken cancellationToken)
        {
            _dbContext.CalendarEvents.Update(calendarEvent);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}