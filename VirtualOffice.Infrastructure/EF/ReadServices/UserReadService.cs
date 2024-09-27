using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> ExistsByIdAsync(Guid id)
        {
            return await _dbContext.Users.AnyAsync(e => e.Id == id);
        }
    }
}