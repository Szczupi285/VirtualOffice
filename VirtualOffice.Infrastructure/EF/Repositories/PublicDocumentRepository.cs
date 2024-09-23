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

        public async Task<PublicDocument> GetByIdAsync(DocumentId guid)
            => await _dbContext.PublicDocuments
            .Include(pd => pd._attachmentFilePaths)
            .Include(pd => pd._eligibleForRead)
            .Include(pd => pd._eligibleForWrite)
            .FirstOrDefaultAsync(pd => pd.Id == guid) ?? throw new PublicDocumentNotFoundException(guid);

        public async Task<PublicDocument> GetByIdAsync(DocumentId guid, CancellationToken cancellationToken)
            => await _dbContext.PublicDocuments
            .Include(pd => pd._attachmentFilePaths)
            .Include(pd => pd._eligibleForRead)
            .Include(pd => pd._eligibleForWrite)
            .FirstOrDefaultAsync(pd => pd.Id == guid) ?? throw new PublicDocumentNotFoundException(guid);

        public async Task AddAsync(PublicDocument publicDocument)
        {
            await _dbContext.PublicDocuments.AddAsync(publicDocument);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAsync(PublicDocument publicDocument, CancellationToken cancellationToken)
        {
            await _dbContext.PublicDocuments.AddAsync(publicDocument, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(PublicDocument publicDocument)
        {
            _dbContext.PublicDocuments.Remove(publicDocument);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(PublicDocument publicDocument, CancellationToken cancellationToken)
        {
            _dbContext.PublicDocuments.Remove(publicDocument);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(PublicDocument publicDocument)
        {
            _dbContext.PublicDocuments.Update(publicDocument);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(PublicDocument publicDocument, CancellationToken cancellationToken)
        {
            _dbContext.PublicDocuments.Update(publicDocument);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}