using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.Repositories;
using VirtualOffice.Domain.Repositories;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Infrastructure.EF.Repositories
{
    public class PrivateDocumentRepository : IPrivateDocumentRepository
    {
        private readonly WriteDbContext _dbContext;

        public PrivateDocumentRepository(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PrivateDocument> GetByIdAsync(DocumentId guid)
            => await _dbContext.PrivateDocuments
            .Include(pd => pd._attachmentFilePaths)
            .FirstOrDefaultAsync(pd => pd.Id == guid) ?? throw new PrivateDocumentNotFoundException(guid);

        public async Task<PrivateDocument> GetByIdAsync(DocumentId guid, CancellationToken cancellationToken)
            => await _dbContext.PrivateDocuments
                .Include(pd => pd._attachmentFilePaths)
                .FirstOrDefaultAsync(pd => pd.Id == guid, cancellationToken) ?? throw new PrivateDocumentNotFoundException(guid);

        public async Task AddAsync(PrivateDocument privateDocument)
        {
            await _dbContext.PrivateDocuments.AddAsync(privateDocument);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAsync(PrivateDocument privateDocument, CancellationToken cancellationToken)
        {
            await _dbContext.PrivateDocuments.AddAsync(privateDocument, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(PrivateDocument privateDocument)
        {
            _dbContext.PrivateDocuments.Remove(privateDocument);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(PrivateDocument privateDocument, CancellationToken cancellationToken)
        {
            _dbContext.PrivateDocuments.Remove(privateDocument);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(PrivateDocument privateDocument)
        {
            _dbContext.PrivateDocuments.Update(privateDocument);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(PrivateDocument privateDocument, CancellationToken cancellationToken)
        {
            _dbContext.PrivateDocuments.Update(privateDocument);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}