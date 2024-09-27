using Microsoft.EntityFrameworkCore;
using VirtualOffice.Application.Services;

namespace VirtualOffice.Infrastructure.EF.ReadServices
{
    public class MeetingReadService : IMeetingReadService
    {
        private readonly WriteDbContext _dbContext;

        public MeetingReadService(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsByIdAsync(Guid id)
        {
            return await _dbContext.Meetings.AnyAsync(e => e.Id.Value == id);
        }
    }
}