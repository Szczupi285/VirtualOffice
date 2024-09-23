using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Domain.Repositories
{
    public interface IMeetingRepository
    {
        Task<Meeting> GetByIdAsync(ScheduleItemId guid);

        Task<Meeting> GetByIdAsync(ScheduleItemId guid, CancellationToken cancellationToken);

        Task AddAsync(Meeting meeting);

        Task AddAsync(Meeting meeting, CancellationToken cancellationToken);

        Task UpdateAsync(Meeting meeting);

        Task UpdateAsync(Meeting meeting, CancellationToken cancellationToken);

        Task DeleteAsync(Meeting meeting);

        Task DeleteAsync(Meeting meeting, CancellationToken cancellationToken);
    }
}