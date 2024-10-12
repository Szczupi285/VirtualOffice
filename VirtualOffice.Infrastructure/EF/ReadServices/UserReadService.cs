﻿using Microsoft.EntityFrameworkCore;
using VirtualOffice.Application.Services;

namespace VirtualOffice.Infrastructure.EF.ReadServices
{
    public class UserReadService : IUserReadService
    {
        private readonly WriteDbContext _dbContext;

        public UserReadService(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Employees.AnyAsync(e => e.Id.Equals(id));
        }
    }
}