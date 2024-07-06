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
        Task<EmployeeTask> GetById(ScheduleItemId guid);
        Task Add(EmployeeTask employeeTask);
        Task Update(EmployeeTask employeeTask);
        Task Delete(ScheduleItemId guidid);
        Task<IEnumerable<EmployeeTask>> GetAllForUser(ApplicationUserId userId);
        Task<IEnumerable<EmployeeTask>> GetAllForUserFutureEvents(ApplicationUserId userId);
        Task<IEnumerable<EmployeeTask>> GetAllForUserByDate(ApplicationUserId userId, ScheduleItemStartDate startDate, ScheduleItemEndDate endDate);
        Task<IEnumerable<EmployeeTask>> GetAllForUserByPriority(ApplicationUserId userId, EmployeeTaskPriorityEnum priority);
        Task<IEnumerable<EmployeeTask>> GetAllForUserByPriorityAndDate(ApplicationUserId userId, ScheduleItemStartDate startDate, ScheduleItemEndDate endDate, EmployeeTaskPriorityEnum priority);
        Task<IEnumerable<EmployeeTask>> GetAllForUserByStatus(ApplicationUserId userId, EmployeeTaskStatusEnum priority);
        Task<IEnumerable<EmployeeTask>> GetAllForUserByStatusAndDate(ApplicationUserId userId, ScheduleItemStartDate startDate, ScheduleItemEndDate endDate, EmployeeTaskStatusEnum priority);

    }
}
