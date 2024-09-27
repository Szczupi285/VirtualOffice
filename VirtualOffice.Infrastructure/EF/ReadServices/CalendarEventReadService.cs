using Microsoft.EntityFrameworkCore;
using VirtualOffice.Application.Services;

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
            return await _dbContext.CalendarEvents.AnyAsync(e => e.Id.Value == id);
        }
    }
}