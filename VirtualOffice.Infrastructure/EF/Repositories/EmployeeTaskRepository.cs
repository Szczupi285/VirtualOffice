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

        public async Task<EmployeeTask> GetById(ScheduleItemId guid)
            => await _dbContext.EmployeeTasks
            .Include(c => c._AssignedEmployees)
            .FirstOrDefaultAsync(c => c.Id == guid) ?? throw new EmployeeTaskNotFoundException(guid);

        public async Task<EmployeeTask> GetById(ScheduleItemId guid, CancellationToken cancellationToken)
             => await _dbContext.EmployeeTasks
            .Include(c => c._AssignedEmployees)
            .FirstOrDefaultAsync(c => c.Id == guid, cancellationToken) ?? throw new EmployeeTaskNotFoundException(guid);

        public async Task AddAsync(EmployeeTask employeeTask)
        {
            await _dbContext.EmployeeTasks.AddAsync(employeeTask);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAsync(EmployeeTask employeeTask, CancellationToken cancellationToken)
        {
            await _dbContext.EmployeeTasks.AddAsync(employeeTask, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(EmployeeTask employeeTask)
        {
            _dbContext.EmployeeTasks.Remove(employeeTask);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(EmployeeTask employeeTask, CancellationToken cancellationToken)
        {
            _dbContext.EmployeeTasks.Remove(employeeTask);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(EmployeeTask employeeTask)
        {
            _dbContext.EmployeeTasks.Update(employeeTask);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(EmployeeTask employeeTask, CancellationToken cancellationToken)
        {
            _dbContext.EmployeeTasks.Update(employeeTask);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}