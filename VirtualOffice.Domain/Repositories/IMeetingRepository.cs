using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Domain.Repositories
{
    public interface IMeetingRepository
    {
        Task<Meeting> GetByIdAsync(ScheduleItemId guid, CancellationToken cancellationToken = default);

        Task AddAsync(Meeting meeting, CancellationToken cancellationToken = default);

        Task UpdateAsync(Meeting meeting, CancellationToken cancellationToken = default);

        Task DeleteAsync(Meeting meeting, CancellationToken cancellationToken = default);
    }
}