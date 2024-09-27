using Microsoft.EntityFrameworkCore;
using VirtualOffice.Application.Services;

namespace VirtualOffice.Infrastructure.EF.ReadServices
{
    public class OrganizationReadService : IOrganizationReadService
    {
        private readonly WriteDbContext _dbContext;

        public OrganizationReadService(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsByIdAsync(Guid id)
        {
            return await _dbContext.Organizations.AnyAsync(e => e.Id.Value == id);
        }
    }
}