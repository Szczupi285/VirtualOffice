using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Domain.Repositories
{
    public interface IEmployeeTaskRepository
    {
        Task<EmployeeTask> GetById(ScheduleItemId guid);

        Task AddAsync(EmployeeTask employeeTask);

        Task UpdateAsync(EmployeeTask employeeTask);

        Task DeleteAsync(ScheduleItemId guidid);
    }
}