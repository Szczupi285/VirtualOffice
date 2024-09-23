using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.Repositories
{
    public interface IPrivateDocumentRepository
    {
        Task<PrivateDocument> GetByIdAsync(DocumentId guid);

        Task<PrivateDocument> GetByIdAsync(DocumentId guid, CancellationToken cancellationToken);

        Task AddAsync(PrivateDocument privateDocument);

        Task AddAsync(PrivateDocument privateDocument, CancellationToken cancellationToken);

        Task UpdateAsync(PrivateDocument privateDocument);

        Task UpdateAsync(PrivateDocument privateDocument, CancellationToken cancellationToken);

        Task DeleteAsync(PrivateDocument privateDocument);

        Task DeleteAsync(PrivateDocument privateDocument, CancellationToken cancellationToken);
    }
}