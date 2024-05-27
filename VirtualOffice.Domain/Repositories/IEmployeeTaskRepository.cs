using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.Repositories
{
    public interface IEmployeeTaskRepository
    {
        EmployeeTask GetById(Guid guid);
        void Add(EmployeeTask user);
        void Update(EmployeeTask user);
        void Delete(Guid id);
        IEnumerable<EmployeeTask> GetAllForUser(Guid userId);
        IEnumerable<EmployeeTask> GetAllForUserFutureEvents(Guid userId);
        IEnumerable<EmployeeTask> GetAllForUserByDate(Guid userId, DateTime startDate, DateTime endDate);
        IEnumerable<EmployeeTask> GetAllForUserByPriority(Guid userId, EmployeeTaskPriorityEnum priority);
        IEnumerable<EmployeeTask> GetAllForUserByPriorityAndDate(Guid userId, DateTime startDate, DateTime endDate, EmployeeTaskPriorityEnum priority);
        IEnumerable<EmployeeTask> GetAllForUserByStatus(Guid userId, EmployeeTaskStatusEnum priority);
        IEnumerable<EmployeeTask> GetAllForUserByStatusAndDate(Guid userId, DateTime startDate, DateTime endDate, EmployeeTaskStatusEnum priority);

    }
}
