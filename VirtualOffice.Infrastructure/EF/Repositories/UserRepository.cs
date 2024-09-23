using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        public async Task<ApplicationUser> GetByIdAsync(ApplicationUserId id)
            => await _dbContext.Employees
            .FirstOrDefaultAsync(e => e.Id == id) ?? throw new EmployeeNotFoundException(id);

        public async Task<ApplicationUser> GetByIdAsync(ApplicationUserId id, CancellationToken cancellationToken)
            => await _dbContext.Employees
            .FirstOrDefaultAsync(e => e.Id == id) ?? throw new EmployeeNotFoundException(id);

        public async Task AddAsync(ApplicationUser user)
        {
            await _dbContext.Employees.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            await _dbContext.Employees.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(ApplicationUser user)
        {
            _dbContext.Employees.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            _dbContext.Employees.Remove(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(ApplicationUser user)
        {
            _dbContext.Employees.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            _dbContext.Employees.Update(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}