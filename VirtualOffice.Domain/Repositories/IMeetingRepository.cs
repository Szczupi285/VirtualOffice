using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Domain.Repositories
{
    public interface IMeetingRepository
    {
        Task<Meeting> GetById(ScheduleItemId guid);

        Task AddAsync(Meeting meeting);

        Task UpdateAsync(Meeting meeting);

        Task DeleteAsync(ScheduleItemId guid);
    }
}