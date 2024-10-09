using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Domain.Repositories
{
    public interface IEmployeeTaskRepository
    {
        Task<EmployeeTask> GetByIdAsync(ScheduleItemId guid, CancellationToken cancellationToken = default);

        Task AddAsync(EmployeeTask employeeTask, CancellationToken cancellationToken = default);

        Task DeleteAsync(EmployeeTask employeeTask, CancellationToken cancellationToken = default);

        Task UpdateAsync(EmployeeTask employeeTask, CancellationToken cancellationToken = default);
    }
}