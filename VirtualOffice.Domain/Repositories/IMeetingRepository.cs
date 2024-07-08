using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Domain.Repositories
{
    public interface IMeetingRepository
    {
        Meeting GetById(ScheduleItemId guid);
        Task Add(Meeting meeting);
        Task Update(Meeting meeting);
        Task Delete(ScheduleItemId guid);
        Task<IEnumerable<Meeting>> GetAllForUser(ApplicationUser userId);
        Task<IEnumerable<Meeting>> GetAllForUserFutureEvents(ApplicationUser userId);
        Task<IEnumerable<Meeting>> GetAllForUserByDate(ApplicationUser userId, ScheduleItemStartDate startDate, ScheduleItemEndDate endDate);
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
