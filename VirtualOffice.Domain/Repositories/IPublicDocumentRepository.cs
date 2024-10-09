using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.Repositories
{
    public interface IPublicDocumentRepository
    {
        Task<PublicDocument> GetByIdAsync(DocumentId guid, CancellationToken cancellationToken = default);

        Task AddAsync(PublicDocument publicDocument, CancellationToken cancellationToken = default);

        Task UpdateAsync(PublicDocument publicDocument, CancellationToken cancellationToken = default);

        Task DeleteAsync(PublicDocument publicDocument, CancellationToken cancellationToken = default);
    }
}