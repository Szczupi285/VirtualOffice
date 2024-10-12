using Microsoft.EntityFrameworkCore;
using VirtualOffice.Application.Services;

namespace VirtualOffice.Infrastructure.EF.ReadServices
{
    public class PublicDocumentReadService : IPublicDocumentReadService
    {
        private readonly WriteDbContext _dbContext;

        public PublicDocumentReadService(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.PublicDocuments.AnyAsync(e => e.Id.Value == id);
        }
    }
}