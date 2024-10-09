using Microsoft.EntityFrameworkCore;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.Repositories;
using VirtualOffice.Domain.Repositories;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Infrastructure.EF.Repositories
{
    public class EmployeeTaskRepository : IEmployeeTaskRepository
    {
        private readonly WriteDbContext _dbContext;

        public EmployeeTaskRepository(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<EmployeeTask> GetByIdAsync(ScheduleItemId guid, CancellationToken cancellationToken = default)
             => await _dbContext.EmployeeTasks
            .Include(e => e._AssignedEmployees)
            .FirstOrDefaultAsync(c => c.Id == guid, cancellationToken) ?? throw new EmployeeTaskNotFoundException(guid);

        public async Task AddAsync(EmployeeTask employeeTask, CancellationToken cancellationToken = default)
        {
            await _dbContext.EmployeeTasks.AddAsync(employeeTask, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(EmployeeTask employeeTask, CancellationToken cancellationToken = default)
        {
            _dbContext.EmployeeTasks.Remove(employeeTask);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(EmployeeTask employeeTask, CancellationToken cancellationToken = default)
        {
            _dbContext.EmployeeTasks.Update(employeeTask);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}