using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.Repositories
{
    public interface IPrivateDocumentRepository
    {
        Task<PrivateDocument> GetById(DocumentId guid);

        Task<PrivateDocument> GetPreviousVersion(DocumentId guid);

        Task Add(PrivateDocument PrivateDocument);

        Task Update(PrivateDocument PrivateDocument);

        Task Delete(DocumentId guid);

        Task<IEnumerable<PrivateDocument>> GetAllForUser(ApplicationUser userId);

        Task<IEnumerable<PrivateDocument>> GetAllSortedForUser(ApplicationUser userId);

        Task<IEnumerable<PublicDocument>> GetAllForUserByDate(ApplicationUserId userId, DocumentCreationDate date);

        Task SaveAsync(CancellationToken cancellationToken);
    }
}