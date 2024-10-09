using Microsoft.EntityFrameworkCore;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.Repositories;
using VirtualOffice.Domain.Repositories;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Infrastructure.EF.Repositories
{
    public class PublicDocumentRepository : IPublicDocumentRepository
    {
        private readonly WriteDbContext _dbContext;

        public PublicDocumentRepository(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PublicDocument> GetByIdAsync(DocumentId guid, CancellationToken cancellationToken = default)
            => await _dbContext.PublicDocuments
            .Include(pd => pd._attachmentFilePaths)
            .Include(pd => pd._eligibleForRead)
            .Include(pd => pd._eligibleForWrite)
            .FirstOrDefaultAsync(pd => pd.Id == guid) ?? throw new PublicDocumentNotFoundException(guid);

        public async Task AddAsync(PublicDocument publicDocument, CancellationToken cancellationToken = default)
        {
            await _dbContext.PublicDocuments.AddAsync(publicDocument, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(PublicDocument publicDocument, CancellationToken cancellationToken = default)
        {
            _dbContext.PublicDocuments.Remove(publicDocument);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(PublicDocument publicDocument, CancellationToken cancellationToken = default)
        {
            _dbContext.PublicDocuments.Update(publicDocument);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}