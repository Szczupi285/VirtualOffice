using Microsoft.EntityFrameworkCore;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.Repositories;
using VirtualOffice.Domain.Repositories;
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

        public async Task<CalendarEvent> GetByIdAsync(ScheduleItemId guid, CancellationToken cancellationToken = default)
            => await _dbContext.CalendarEvents.
            Include(c => c._AssignedEmployees)
            .FirstOrDefaultAsync(c => c.Id == guid, cancellationToken) ?? throw new CalendarEventNotFoundException(guid);

        public async Task AddAsync(CalendarEvent calendarEvent, CancellationToken cancellationToken = default)
        {
            await _dbContext.CalendarEvents.AddAsync(calendarEvent, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(CalendarEvent calendarEvent, CancellationToken cancellationToken = default)
        {
            _dbContext.CalendarEvents.Remove(calendarEvent);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(CalendarEvent calendarEvent, CancellationToken cancellationToken = default)
        {
            _dbContext.CalendarEvents.Update(calendarEvent);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}