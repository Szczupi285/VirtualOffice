using Microsoft.EntityFrameworkCore;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.Repositories;
using VirtualOffice.Domain.Repositories;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Infrastructure.EF.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WriteDbContext _dbContext;

        public UserRepository(WriteDbContext writeDbContext)
        {
            _dbContext = writeDbContext;
        }

        public async Task<ApplicationUser> GetByIdAsync(ApplicationUserId id, CancellationToken cancellationToken = default)
            => await _dbContext.Employees
            .FirstOrDefaultAsync(e => e.Id == id) ?? throw new EmployeeNotFoundException(id);

        public async Task AddAsync(ApplicationUser user, CancellationToken cancellationToken = default)
        {
            await _dbContext.Employees.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(ApplicationUser user, CancellationToken cancellationToken = default)
        {
            _dbContext.Employees.Remove(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(ApplicationUser user, CancellationToken cancellationToken = default)
        {
            _dbContext.Employees.Update(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}