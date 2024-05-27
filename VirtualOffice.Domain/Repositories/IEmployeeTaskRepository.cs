using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Domain.Repositories
{
    public interface IEmployeeTaskRepository
    {
        EmployeeTask GetById(ScheduleItemId guid);
        void Add(EmployeeTask user);
        void Update(EmployeeTask user);
        void Delete(ScheduleItemId id);
        IEnumerable<EmployeeTask> GetAllForUser(ApplicationUserId userId);
        IEnumerable<EmployeeTask> GetAllForUserFutureEvents(ApplicationUserId userId);
        IEnumerable<EmployeeTask> GetAllForUserByDate(ApplicationUserId userId, ScheduleItemStartDate startDate, ScheduleItemEndDate endDate);
        IEnumerable<EmployeeTask> GetAllForUserByPriority(ApplicationUserId userId, EmployeeTaskPriorityEnum priority);
        IEnumerable<EmployeeTask> GetAllForUserByPriorityAndDate(ApplicationUserId userId, ScheduleItemStartDate startDate, ScheduleItemEndDate endDate, EmployeeTaskPriorityEnum priority);
        IEnumerable<EmployeeTask> GetAllForUserByStatus(ApplicationUserId userId, EmployeeTaskStatusEnum priority);
        IEnumerable<EmployeeTask> GetAllForUserByStatusAndDate(ApplicationUserId userId, ScheduleItemStartDate startDate, ScheduleItemEndDate endDate, EmployeeTaskStatusEnum priority);

    }
}
