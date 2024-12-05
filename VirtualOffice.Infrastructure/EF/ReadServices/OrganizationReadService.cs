using Microsoft.EntityFrameworkCore;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.ValueObjects.Organization;

namespace VirtualOffice.Infrastructure.EF.ReadServices
{
    public class OrganizationReadService : IOrganizationReadService
    {
        private readonly WriteDbContext _dbContext;

        public OrganizationReadService(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Organizations.AnyAsync(e => e.Id == new OrganizationId(id));
        }
    }
}