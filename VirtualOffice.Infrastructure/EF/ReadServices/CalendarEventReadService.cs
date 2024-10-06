using Microsoft.EntityFrameworkCore;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Infrastructure.EF.ReadServices
{
    public class CalendarEventReadService : ICalendarEventReadService
    {
        private readonly WriteDbContext _dbContext;

        public CalendarEventReadService(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsByIdAsync(Guid id)
        {
            // temporary? implementing IEquatable also requires to impelement IConvertible.
            return await _dbContext.CalendarEvents.AnyAsync(e => e.Id == new ScheduleItemId(id));
        }
    }
}