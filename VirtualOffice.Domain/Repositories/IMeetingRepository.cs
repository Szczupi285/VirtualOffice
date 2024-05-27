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
        void Add(Meeting user);
        void Update(Meeting user);
        void Delete(ScheduleItemId id);
        IEnumerable<Meeting> GetAllForUser(ApplicationUser userId);
        IEnumerable<Meeting> GetAllForUserFutureEvents(ApplicationUser userId);
        IEnumerable<Meeting> GetAllForUserByDate(ApplicationUser userId, ScheduleItemStartDate startDate, ScheduleItemEndDate endDate);
    }
}
