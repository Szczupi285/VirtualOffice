﻿using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Domain.Repositories
{
    public interface IEmployeeTaskRepository
    {
        Task<EmployeeTask> GetByIdAsync(ScheduleItemId guid, CancellationToken cancellationToken);

        Task AddAsync(EmployeeTask employeeTask, CancellationToken cancellationToken);

        Task DeleteAsync(EmployeeTask employeeTask, CancellationToken cancellationToken);

        Task UpdateAsync(EmployeeTask employeeTask, CancellationToken cancellationToken);
    }
}