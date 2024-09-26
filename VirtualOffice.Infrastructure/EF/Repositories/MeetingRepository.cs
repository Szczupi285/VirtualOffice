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

        public async Task<Meeting> GetByIdAsync(ScheduleItemId guid)
            => await _dbContext.Meetings
            .Include(m => m._AssignedEmployees)
            .FirstOrDefaultAsync(c => c.Id == guid) ?? throw new MeetingNotFoundException(guid);

        public async Task<Meeting> GetByIdAsync(ScheduleItemId guid, CancellationToken cancellationToken)
        => await _dbContext.Meetings
            .Include(m => m._AssignedEmployees)
            .FirstOrDefaultAsync(c => c.Id == guid, cancellationToken) ?? throw new MeetingNotFoundException(guid);

        public async Task AddAsync(Meeting meeting)
        {
            await _dbContext.Meetings.AddAsync(meeting);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAsync(Meeting meeting, CancellationToken cancellationToken)
        {
            await _dbContext.Meetings.AddAsync(meeting, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Meeting meeting)
        {
            _dbContext.Meetings.Remove(meeting);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Meeting meeting, CancellationToken cancellationToken)
        {
            _dbContext.Meetings.Remove(meeting);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Meeting meeting)
        {
            _dbContext.Meetings.Update(meeting);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Meeting meeting, CancellationToken cancellationToken)
        {
            _dbContext.Meetings.Update(meeting);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}