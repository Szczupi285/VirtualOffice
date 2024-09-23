using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Domain.Repositories
{
    public interface IEmployeeTaskRepository
    {
        Task<EmployeeTask> GetById(ScheduleItemId guid);

        Task<EmployeeTask> GetById(ScheduleItemId guid, CancellationToken cancellationToken);

        Task AddAsync(EmployeeTask employeeTask);

        Task AddAsync(EmployeeTask employeeTask, CancellationToken cancellationToken);

        Task DeleteAsync(EmployeeTask employeeTask);

        Task DeleteAsync(EmployeeTask employeeTask, CancellationToken cancellationToken);

        Task UpdateAsync(EmployeeTask employeeTask);

        Task UpdateAsync(EmployeeTask employeeTask, CancellationToken cancellationToken);
    }
}