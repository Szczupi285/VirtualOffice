using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.Repositories
{
    public interface IPublicDocumentRepository
    {
        Task<PublicDocument> GetByIdAsync(DocumentId guid);

        Task<PublicDocument> GetByIdAsync(DocumentId guid, CancellationToken cancellationToken);

        Task AddAsync(PublicDocument publicDocument);

        Task AddAsync(PublicDocument publicDocument, CancellationToken cancellationToken);

        Task UpdateAsync(PublicDocument publicDocument);

        Task UpdateAsync(PublicDocument publicDocument, CancellationToken cancellationToken);

        Task DeleteAsync(PublicDocument publicDocument);

        Task DeleteAsync(PublicDocument publicDocument, CancellationToken cancellationToken);
    }
}