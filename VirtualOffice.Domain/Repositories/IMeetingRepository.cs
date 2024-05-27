using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.Repositories
{
    public interface IMeetingRepository
    {
        Meeting GetById(Guid guid);
        void Add(Meeting user);
        void Update(Meeting user);
        void Delete(Guid id);
        IEnumerable<Meeting> GetAllForUser(Guid userId);
        IEnumerable<Meeting> GetAllForUserFutureEvents(Guid userId);
        IEnumerable<Meeting> GetAllForUserByDate(Guid userId, DateTime startDate, DateTime endDate);
    }
}
