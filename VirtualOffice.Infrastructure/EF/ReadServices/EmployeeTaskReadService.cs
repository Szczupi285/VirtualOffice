using Microsoft.EntityFrameworkCore;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Infrastructure.EF.ReadServices
{
    public class EmployeeTaskReadService : IEmployeeTaskReadService
    {
        private readonly WriteDbContext _dbContext;

        public EmployeeTaskReadService(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.EmployeeTasks.AnyAsync(e => e.Id == new ScheduleItemId(id));
        }
    }
}