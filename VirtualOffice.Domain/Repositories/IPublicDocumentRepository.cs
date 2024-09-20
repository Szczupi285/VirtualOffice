using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.Repositories
{
    public interface IPublicDocumentRepository
    {
        Task<PublicDocument> GetById(DocumentId guid);

        Task AddAsync(PublicDocument PublicDocument);

        Task UpdateAsync(PublicDocument PublicDocument);

        Task DeleteAsync(DocumentId guid);
    }
}