using Microsoft.EntityFrameworkCore;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.Repositories;
using VirtualOffice.Domain.Repositories;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Infrastructure.EF.Repositories
{
    public class MeetingRepository : IMeetingRepository
    {
        private readonly WriteDbContext _dbContext;

        public MeetingRepository(WriteDbContext writeDbContext)
        {
            _dbContext = writeDbContext;
        }

        public async Task<Meeting> GetByIdAsync(ScheduleItemId guid, CancellationToken cancellationToken = default)
        => await _dbContext.Meetings
            .Include(m => m._AssignedEmployees)
            .FirstOrDefaultAsync(c => c.Id == guid, cancellationToken) ?? throw new MeetingNotFoundException(guid);

        public async Task AddAsync(Meeting meeting, CancellationToken cancellationToken = default)
        {
            await _dbContext.Meetings.AddAsync(meeting, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Meeting meeting, CancellationToken cancellationToken = default)
        {
            _dbContext.Meetings.Remove(meeting);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Meeting meeting, CancellationToken cancellationToken = default)
        {
            _dbContext.Meetings.Update(meeting);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}