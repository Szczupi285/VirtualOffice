using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Interfaces;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Domain.Repositories
{
    public interface IEmployeeTaskRepository
    {
        Task<IEmployeeTask> GetById(ScheduleItemId guid);
        Task Add(IEmployeeTask employeeTask);
        Task Update(IEmployeeTask employeeTask);
        Task Delete(ScheduleItemId guidid);
        Task<IEnumerable<IEmployeeTask>> GetAllForUser(ApplicationUserId userId);
        Task<IEnumerable<IEmployeeTask>> GetAllForUserFutureEvents(ApplicationUserId userId);
        Task<IEnumerable<IEmployeeTask>> GetAllForUserByDate(ApplicationUserId userId, ScheduleItemStartDate startDate, ScheduleItemEndDate endDate);
        Task<IEnumerable<IEmployeeTask>> GetAllForUserByPriority(ApplicationUserId userId, EmployeeTaskPriorityEnum priority);
        Task<IEnumerable<IEmployeeTask>> GetAllForUserByPriorityAndDate(ApplicationUserId userId, ScheduleItemStartDate startDate, ScheduleItemEndDate endDate, EmployeeTaskPriorityEnum priority);
        Task<IEnumerable<IEmployeeTask>> GetAllForUserByStatus(ApplicationUserId userId, EmployeeTaskStatusEnum priority);
        Task<IEnumerable<IEmployeeTask>> GetAllForUserByStatusAndDate(ApplicationUserId userId, ScheduleItemStartDate startDate, ScheduleItemEndDate endDate, EmployeeTaskStatusEnum priority);
        Task SaveAsync(CancellationToken cancellationToken);

    }
}
