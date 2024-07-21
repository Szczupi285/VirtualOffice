using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Interfaces;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Domain.Repositories
{
    public interface IMeetingRepository
    {
        IMeeting GetById(ScheduleItemId guid);
        Task Add(IMeeting meeting);
        Task Update(IMeeting meeting);
        Task Delete(ScheduleItemId guid);
        Task<IEnumerable<IMeeting>> GetAllForUser(ApplicationUser userId);
        Task<IEnumerable<IMeeting>> GetAllForUserFutureEvents(ApplicationUser userId);
        Task<IEnumerable<IMeeting>> GetAllForUserByDate(ApplicationUser userId, ScheduleItemStartDate startDate, ScheduleItemEndDate endDate);
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
