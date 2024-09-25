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

        public async Task<CalendarEvent> GetByIdAsync(ScheduleItemId guid)
           => await _dbContext.CalendarEvents.
           Include(c => c._AssignedEmployees)
           .FirstOrDefaultAsync(c => c.Id == guid) ?? throw new CalendarEventNotFoundException(guid);

        public async Task<CalendarEvent> GetByIdAsync(ScheduleItemId guid, CancellationToken cancellationToken)
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
            _dbContext.CalendarEvents.Remove(calendarEvent);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(CalendarEvent calendarEvent, CancellationToken cancellationToken)
        {
            _dbContext.CalendarEvents.Remove(calendarEvent);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(CalendarEvent calendarEvent)
        {
            var currentEntity = _dbContext.CalendarEvents
               .SingleOrDefault(e => e.Id == calendarEvent.Id)
               ?? throw new CalendarEventNotFoundException(calendarEvent.Id);

            // Version is incremented when we call AddEvent which happens in every method that modifies aggregate root.
            // So we subtract 1 from Version of our updated entity since it already has been incremented while modifying the properties.
            // No matter how many properties and how many times we change them, Version can be incremented only by one.
            if (currentEntity.Version != calendarEvent.Version - 1)
                throw new DbUpdateConcurrencyException($"This aggregate root has been modified since it was retrived");

            _dbContext.Entry(currentEntity).CurrentValues.SetValues(calendarEvent);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(CalendarEvent calendarEvent, CancellationToken cancellationToken)
        {
            var currentEntity = _dbContext.CalendarEvents
                .SingleOrDefault(e => e.Id == calendarEvent.Id)
                ?? throw new CalendarEventNotFoundException(calendarEvent.Id);

            // Version is incremented when we call AddEvent which happens in every method that modifies aggregate root.
            // So we subtract 1 from Version of our updated entity since it already has been incremented while modifying the properties.
            // No matter how many properties and how many times we change them, Version can be incremented only by one.
            if (currentEntity.Version != calendarEvent.Version - 1)
                throw new DbUpdateConcurrencyException($"This aggregate root has been modified since it was retrived");

            _dbContext.Entry(currentEntity).CurrentValues.SetValues(calendarEvent);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}